using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Defines methods to refresh the access token.
    /// </summary>
    public interface ITokenRefresher
    {

        /// <summary>
        /// Sets the token.
        /// </summary>
        /// <param name="token">The token.</param>
        void SetToken(ITokenPair token);

        /// <summary>
        /// Gets the access token expiration time.
        /// </summary>
        /// <returns></returns>
        DateTimeOffset? GetTokenExpiration();

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns></returns>
        string GetAccessToken();

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <returns></returns>
        string GetRefreshToken();

        /// <summary>
        /// Gets whether or not the access token needs to refresh.
        /// </summary>
        /// <returns></returns>
        bool NeedRefreshToken();

        /// <summary>
        /// Refreshes the access token.
        /// </summary>
        /// <returns></returns>
        void RefreshToken();

    }

}
