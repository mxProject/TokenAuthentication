using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Define the token provider.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public interface ITokenProvider<TPayload>
    {

        /// <summary>
        /// Creates a new token.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <param name="payload">The payload.</param>
        /// <returns>A token string.</returns>
        string CreateToken(ITokenClaim claim, TPayload payload);

    }

}
