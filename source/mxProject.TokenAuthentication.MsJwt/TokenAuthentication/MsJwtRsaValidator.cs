using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// RSA Token validator.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class MsJwtRsaValidator<TPayload> : ITokenValidator<TPayload>
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="publicKey">The rsa public key.</param>
        public MsJwtRsaValidator(RsaSecurityKey publicKey)
        {
            m_Key = publicKey;

            m_LifetimeValidator = new LifetimeValidator((nbf, exp, token, parameter) =>
            {
                return ValidateLifetime(nbf, exp, out TokenState state, out string message);
            }
            );

        }

        #endregion
        
        private readonly RsaSecurityKey m_Key;
        private readonly JwtSecurityTokenHandler m_TokenHandler = new JwtSecurityTokenHandler();
        private readonly LifetimeValidator m_LifetimeValidator;

        /// <summary>
        /// Gets or sets the valid issuers.
        /// </summary>
        public string[] ValidIssuers { get; set; }

        /// <summary>
        /// Gets or sets the valid audiences.
        /// </summary>
        public string[] ValidAudiences { get; set; }

        /// <summary>
        /// Validates the specified token.
        /// </summary>
        /// <param name="tokenString">The token.</param>
        /// <param name="claim">The claim obtained from token.</param>
        /// <param name="payload">The payload obtained from token.</param>
        /// <param name="tokenState">The state of the token.</param>
        /// <param name="errorMessage">A message indicating that the token is invalid.</param>
        /// <returns>true if it is valid; otherwise false.</returns>
        public bool ValidateToken(string tokenString, out ITokenClaim claim, out TPayload payload, out TokenState tokenState, out string errorMessage)
        {

            TokenValidationParameters parameters = new TokenValidationParameters { };

            parameters.IssuerSigningKey = m_Key;
            parameters.ValidateIssuerSigningKey = true;

            if (ValidIssuers != null && ValidIssuers.Length > 0)
            {
                if (ValidIssuers.Length == 1) { parameters.ValidIssuer = ValidIssuers.First(); }
                else { parameters.ValidIssuers = ValidIssuers; }
                parameters.ValidateIssuer = true;
            }
            else
            {
                parameters.ValidateIssuer = false;
            }

            if (ValidAudiences != null && ValidAudiences.Length > 0)
            {
                if (ValidAudiences.Length == 1) { parameters.ValidAudience = ValidAudiences.First(); }
                else { parameters.ValidAudiences = ValidAudiences; }
                parameters.ValidateAudience = true;
            }
            else
            {
                parameters.ValidateAudience = false;
            }

            parameters.ValidateLifetime = true;
            parameters.LifetimeValidator = m_LifetimeValidator;

            try
            {

                ClaimsPrincipal claims = m_TokenHandler.ValidateToken(tokenString, parameters, out SecurityToken token);

                claim = new TokenClaim()
                {
                    JwtID = GetClaim(claims, KnownJwtClaims.JwtID),
                    Audience = GetClaim(claims, KnownJwtClaims.Audience),
                    Expiration = ToDateTimeOffset(GetClaim(claims, KnownJwtClaims.Expiration)),
                    NotBefore = ToDateTimeOffset(GetClaim(claims, KnownJwtClaims.NotBefore)),
                };

                string payloadJson = claims.FindFirst(KnownJwtClaims.UserPayload)?.Value;

                if (string.IsNullOrEmpty(payloadJson))
                {
                    payload = default(TPayload);
                }
                else
                {
                    payload = Newtonsoft.Json.JsonConvert.DeserializeObject<TPayload>(payloadJson);
                }

                tokenState = TokenState.Valid;
                errorMessage = null;
                return true;

            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                claim = null;
                payload = default(TPayload);
                if (ValidateLifetime(ex.NotBefore, ex.Expires, out tokenState, out errorMessage))
                {
                    tokenState = TokenState.Invalid;
                    errorMessage = "The token is invalid.";
                }
                return false;
            }
            catch( SecurityTokenInvalidIssuerException)
            {
                claim = null;
                payload = default(TPayload);
                tokenState = TokenState.Invalid;
                errorMessage = "The issuer is invalid.";
                return false;
            }
            catch (SecurityTokenInvalidAudienceException)
            {
                claim = null;
                payload = default(TPayload);
                tokenState = TokenState.Invalid;
                errorMessage = "The audience is invalid.";
                return false;
            }
            catch (Exception)
            {
                claim = null;
                payload = default(TPayload);
                tokenState = TokenState.Invalid;
                errorMessage = "The token is invalid.";
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetClaim(ClaimsPrincipal claims, string key)
        {
            foreach (Claim claim in claims.Claims)
            {
                if (string.Compare(claim.Type, key, true) == 0)
                {
                    return claim.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbf"></param>
        /// <param name="exp"></param>
        /// <param name="state"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool ValidateLifetime(DateTime? nbf, DateTime? exp, out TokenState state, out string message)
        {
            DateTime now = DateTime.Now.ToUniversalTime();

            if (exp.HasValue && exp.Value < now)
            {
                state = TokenState.Expired;
                message = "The token is expired.";
                return false;
            }

            if (nbf.HasValue && nbf.Value > now)
            {
                state = TokenState.NotBefore;
                message = "The token is not yet valid.";
                return false;
            }

            state = TokenState.Valid;
            message = null;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private DateTimeOffset ToDateTimeOffset(string seconds)
        {
            DateTimeOffset? date = ToDateTimeOffsetOrNull(seconds);
            return date ?? default(DateTimeOffset);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private DateTimeOffset? ToDateTimeOffsetOrNull(string seconds)
        {
            if (string.IsNullOrEmpty(seconds)) { return null; }
            return DateTimeOffset.FromUnixTimeSeconds(long.Parse(seconds));
        }

    }

}
