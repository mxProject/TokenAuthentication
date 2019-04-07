using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Create a provider and validator.
    /// </summary>
    public static class MsJwtFactory
    {

        #region RS256

        /// <summary>
        /// Create a RS256 token provider.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="privateKey">The RS256 private key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <returns></returns>
        public static MsJwtRs256Provider<TPayload> CreateRs256Provider<TPayload>(string privateKey, string issuer)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlStringEx(privateKey);

            RsaSecurityKey key = new RsaSecurityKey(rsa);

            return new MsJwtRs256Provider<TPayload>(key)
            {
                Issuer = issuer
            };

        }

        /// <summary>
        /// Create a RS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="publicKey">The RS256 public key</param>
        /// <param name="validIssuer">The valid issuer.</param>
        /// <param name="validAudience">The valid audience.</param>
        /// <returns></returns>
        public static MsJwtRs256Validator<TPayload> CreateRs256Validator<TPayload>(string publicKey, string validIssuer, string validAudience)
        {
            return CreateRs256Validator<TPayload>(publicKey
                , string.IsNullOrEmpty(validIssuer) ? null : new string[] { validIssuer }
                , string.IsNullOrEmpty(validAudience) ? null : new string[] { validAudience }
                );
        }

        /// <summary>
        /// Create a RS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="publicKey">The RS256 public key</param>
        /// <param name="validIssuer">The valid issuer.</param>
        /// <param name="validAudiences">The valid audiences.</param>
        /// <returns></returns>
        public static MsJwtRs256Validator<TPayload> CreateRs256Validator<TPayload>(string publicKey, string validIssuer, string[] validAudiences)
        {
            return CreateRs256Validator<TPayload>(publicKey
                , string.IsNullOrEmpty(validIssuer) ? null : new string[] { validIssuer }
                , validAudiences
                );
        }

        /// <summary>
        /// Create a RS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="publicKey">The RS256 public key</param>
        /// <param name="validIssuers">The valid issuers.</param>
        /// <param name="validAudiences">The valid audiences.</param>
        /// <returns></returns>
        public static MsJwtRs256Validator<TPayload> CreateRs256Validator<TPayload>(string publicKey, string[] validIssuers, string[] validAudiences)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlStringEx(publicKey);

            RsaSecurityKey key = new RsaSecurityKey(rsa);

            return new MsJwtRs256Validator<TPayload>(key)
            {
                ValidIssuers = validIssuers,
                ValidAudiences = validAudiences
            };

        }

        #endregion

        #region HS256

        /// <summary>
        /// Create a HS256 token provider.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="commonKey">The HS256 common key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <returns></returns>
        public static MsJwtHs256Provider<TPayload> CreateHs256Provider<TPayload>(string commonKey, string issuer)
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(commonKey));

            return new MsJwtHs256Provider<TPayload>(key)
            {
                Issuer = issuer
            };

        }

        /// <summary>
        /// Create a RS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="commonKey">The HS256 common key</param>
        /// <param name="validIssuer">The valid issuer.</param>
        /// <param name="validAudience">The valid audience.</param>
        /// <returns></returns>
        public static MsJwtHs256Validator<TPayload> CreateHs256Validator<TPayload>(string commonKey, string validIssuer, string validAudience)
        {
            return CreateHs256Validator<TPayload>(commonKey
                , string.IsNullOrEmpty(validIssuer) ? null : new string[] { validIssuer }
                , string.IsNullOrEmpty(validAudience) ? null : new string[] { validAudience }
                );
        }

        /// <summary>
        /// Create a RS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="commonKey">The HS256 common key</param>
        /// <param name="validIssuer">The valid issuer.</param>
        /// <param name="validAudiences">The valid audiences.</param>
        /// <returns></returns>
        public static MsJwtHs256Validator<TPayload> CreateHs256Validator<TPayload>(string commonKey, string validIssuer, string[] validAudiences)
        {
            return CreateHs256Validator<TPayload>(commonKey
                , string.IsNullOrEmpty(validIssuer) ? null : new string[] { validIssuer }
                , validAudiences
                );
        }

        /// <summary>
        /// Create a HS256 token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="commonKey">The HS256 common key</param>
        /// <param name="validIssuers">The valid issuers.</param>
        /// <param name="validAudiences">The valid audiences.</param>
        /// <returns></returns>
        public static MsJwtHs256Validator<TPayload> CreateHs256Validator<TPayload>(string commonKey, string[] validIssuers, string[] validAudiences)
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(commonKey));

            return new MsJwtHs256Validator<TPayload>(key)
            {
                ValidIssuers = validIssuers,
                ValidAudiences = validAudiences
            };

        }

        #endregion

    }

}
