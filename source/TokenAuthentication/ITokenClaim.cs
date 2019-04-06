using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Define the claim of the token.
    /// </summary>
    public interface ITokenClaim
    {

        /// <summary>
        /// Gets the ID.
        /// </summary>
        string JwtID { get; }

        /// <summary>
        /// Gets the audience.
        /// </summary>
        string Audience { get; }

        /// <summary>
        /// Gets the expiration time.
        /// </summary>
        DateTimeOffset Expiration { get; }

        /// <summary>
        /// Gets the notbefore time.
        /// </summary>
        DateTimeOffset NotBefore { get; }

    }

}
