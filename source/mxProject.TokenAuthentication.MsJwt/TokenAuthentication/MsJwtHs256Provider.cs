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
    /// HS256 token provider.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public sealed class MsJwtHs256Provider<TPayload> : MsJwtProviderBase<TPayload>
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="commonKey">The HS256 common key.</param>
        public MsJwtHs256Provider(SymmetricSecurityKey commonKey) : base()
        {
            m_Credentials = new SigningCredentials(commonKey, SecurityAlgorithms.HmacSha256);
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
