using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Basic implementation of token provider.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public abstract class MsJwtProviderBase<TPayload> : ITokenProvider<TPayload>
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        protected MsJwtProviderBase()
        {
        }

        #endregion

        private readonly JwtSecurityTokenHandler m_TokenHandler = new JwtSecurityTokenHandler();

        /// <summary>
        /// Gets or sets the issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Creates a new token.
        /// </summary>
        /// <param name="claim">The new token information.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>A token string.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="claim"/>> is null.
        /// </exception>
        public string CreateToken(ITokenClaim claim, TPayload payload)
        {

            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            var descriptor = new SecurityTokenDescriptor { };

            descriptor.SigningCredentials = GetSigningCredentials();
            descriptor.Issuer = Issuer;
            descriptor.Audience = claim.Audience;
            descriptor.Expires = claim.Expiration.UtcDateTime;
            descriptor.NotBefore = claim.NotBefore.UtcDateTime;
            descriptor.IssuedAt = DateTime.UtcNow;

            ClaimsIdentity claims = new ClaimsIdentity();

            if (!string.IsNullOrEmpty(claim.JwtID))
            {
                claims.AddClaim(new Claim(KnownJwtClaims.JwtID, claim.JwtID));
            }

            if (payload != null)
            {
                claims.AddClaim(new Claim(KnownJwtClaims.UserPayload, Newtonsoft.Json.JsonConvert.SerializeObject(payload)));
            }

            descriptor.Subject = claims;

            JwtSecurityToken token = m_TokenHandler.CreateJwtSecurityToken(descriptor);

            string tokenString = m_TokenHandler.WriteToken(token);

            return tokenString;

        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <returns></returns>
        protected abstract SigningCredentials GetSigningCredentials();

    }

}
