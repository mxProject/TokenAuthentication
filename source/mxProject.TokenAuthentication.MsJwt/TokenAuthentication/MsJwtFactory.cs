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

        /// <summary>
        /// Create a rsa token provider.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="privateKey">The rsa private key.</param>
        /// <returns></returns>
        public static MsJwtRsaProvider<TPayload> CreateRsaProvider<TPayload>(string privateKey)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlStringEx(privateKey);

            RsaSecurityKey key = new RsaSecurityKey(rsa);
            
            return new MsJwtRsaProvider<TPayload>(key);

        }

        /// <summary>
        /// Create a rsa token validator.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="publicKey">The rsa public key</param>
        /// <returns></returns>
        public static MsJwtRsaValidator<TPayload> CreateRsaValidator<TPayload>(string publicKey)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.FromXmlStringEx(publicKey);

            RsaSecurityKey key = new RsaSecurityKey(rsa);

            return new MsJwtRsaValidator<TPayload>(key);

        }

    }

}
