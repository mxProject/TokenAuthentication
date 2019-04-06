using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Define the token validator.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public interface ITokenValidator<TPayload>
    {

        /// <summary>
        /// Validates the specified token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="claim">The claim obtained from token.</param>
        /// <param name="payload">The payload obtained from token.</param>
        /// <param name="tokenState">The state of the token.</param>
        /// <param name="errorMessage">A message indicating that the token is invalid.</param>
        /// <returns>true if it is valid; otherwise false.</returns>
        bool ValidateToken(string token, out ITokenClaim claim, out TPayload payload, out TokenState tokenState, out string errorMessage);

    }

}
