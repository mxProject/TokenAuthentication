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
    /// RS256 token provider.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class MsJwtRs256Provider<TPayload> : MsJwtProviderBase<TPayload>
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="privateKey">The RS256 private key.</param>
        public MsJwtRs256Provider(RsaSecurityKey privateKey) : base()
        {
            m_Credentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256);
        }

        #endregion

        private readonly SigningCredentials m_Credentials;

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <returns></returns>
        protected override SigningCredentials GetSigningCredentials()
        {
            return m_Credentials;
        }

    }

}
