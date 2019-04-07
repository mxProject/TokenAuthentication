using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Manages token updates.
    /// </summary>
    public class TokenManager : ITokenManager
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="refreshMethod">The method that refresh the token.</param>
        /// <param name="secondsBefore">how many seconds before the expiration time to refresh.</param>
        public TokenManager(RefreshTokenDelegate refreshMethod, int secondsBefore)
        {
            RefreshMethod = refreshMethod;
            SecondsBefore = secondsBefore;
        }

        #endregion

        /// <summary>
        /// Gets a method that refresh the token.
        /// </summary>
        protected RefreshTokenDelegate RefreshMethod { get; }

        /// <summary>
        /// Gets how many seconds before the expiration time to refresh.
        /// </summary>
        public int SecondsBefore { get; }
        
        #region token

        /// <summary>
        /// Gets the current access token.
        /// </summary>
        protected ITokenInfo CurrentAccessToken { get; private set; }

        /// <summary>
        /// Gets the current refresh token.
        /// </summary>
        protected ITokenInfo CurrentRefreshToken { get; private set; }

        /// <summary>
        /// Sets the token.
        /// </summary>
        /// <param name="token"></param>
        public void SetToken(ITokenPair token)
        {
            CurrentAccessToken = token?.AccessToken;
            CurrentRefreshToken = token?.RefreshToken;
        }

        /// <summary>
        /// Gets the access token expiration time.
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? GetTokenExpiration()
        {
            return CurrentAccessToken?.Expiration;
        }

        /// <summary>
        /// Gets the access token.
        /// </summary>
        /// <returns></returns>
        public  string GetAccessToken()
        {
            return CurrentAccessToken?.TokenString;
        }

        /// <summary>
        /// Gets the refresh token.
        /// </summary>
        /// <returns></returns>
        public string GetRefreshToken()
        {
            return CurrentRefreshToken?.TokenString;
        }

        #endregion

        #region refresh

        /// <summary>
        /// Gets whether or not the access token needs to refresh.
        /// </summary>
        /// <returns></returns>
        public bool NeedRefreshToken()
        {

            DateTimeOffset? expireation = GetTokenExpiration();

            if (!expireation.HasValue) { return false; }

            return (expireation.Value <= DateTimeOffset.UtcNow.AddSeconds(SecondsBefore));

        }

        /// <summary>
        /// Refreshes the access token.
        /// </summary>
        /// <returns></returns>
        public void RefreshToken()
        {

            ITokenPair newToken = RefreshToken(CurrentRefreshToken?.TokenString);

            SetToken(newToken);

        }

        /// <summary>
        /// Refreshes the access token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>A new token.</returns>
        protected virtual ITokenPair RefreshToken(string token)
        {
            return RefreshMethod(token);
        }

        #endregion

    }

}
