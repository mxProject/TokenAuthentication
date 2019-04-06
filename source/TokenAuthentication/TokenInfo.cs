using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Token information.
    /// </summary>
    public class TokenInfo : ITokenInfo
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="tokenString">The token string.</param>
        /// <param name="expiration">The token expiration time.</param>
        /// <param name="notBefore">The token notbefore time.</param>
        public TokenInfo(string tokenString, DateTimeOffset expiration, DateTimeOffset notBefore)
        {
            TokenString = tokenString;
            Expiration = expiration;
            NotBefore = notBefore;
        }

        /// <summary>
        /// Gets the token string.
        /// </summary>
        /// <returns></returns>
        public string TokenString { get; }

        /// <summary>
        /// Gets the token expiration time.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset Expiration { get; }

        /// <summary>
        /// Gets the token notbefore time.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset NotBefore { get; }

    }

}
