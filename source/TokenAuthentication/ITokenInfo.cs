using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Defines the members of the token.
    /// </summary>
    public interface ITokenInfo
    {

        /// <summary>
        /// Gets the token string.
        /// </summary>
        /// <returns></returns>
        string TokenString { get; }

        /// <summary>
        /// Gets the token expiration time.
        /// </summary>
        /// <returns></returns>
        DateTimeOffset Expiration { get; }

        /// <summary>
        /// Gets the token notbefore time.
        /// </summary>
        /// <returns></returns>
        DateTimeOffset NotBefore { get; }

    }

}
