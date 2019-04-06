using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Pair of access token and refresh token.
    /// </summary>
    public interface ITokenPair
    {

        /// <summary>
        /// Gets the access token.
        /// </summary>
        ITokenInfo AccessToken { get; }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        ITokenInfo RefreshToken { get; }

    }

}
