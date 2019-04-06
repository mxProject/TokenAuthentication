using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Pair of access token and refresh token.
    /// </summary>
    public class TokenPair : ITokenPair
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="refreshToken">The refresh token.</param>
        public TokenPair(ITokenInfo accessToken, ITokenInfo refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        public ITokenInfo AccessToken { get; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        public ITokenInfo RefreshToken { get; }

    }

}
