using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// State that indicating whether the token is valid.
    /// </summary>
    public enum TokenState
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The token is valid.
        /// </summary>
        Valid,

        /// <summary>
        /// The Token is invalid for some reason.
        /// </summary>
        Invalid,

        /// <summary>
        /// The issuer is invalid.
        /// </summary>
        InvalidIssuer,

        /// <summary>
        /// The audience is invalid.
        /// </summary>
        InvalidAudience,

        /// <summary>
        /// The token is not valid yet.
        /// </summary>
        NotBefore,

        /// <summary>
        /// Token has expired.
        /// </summary>
        Expired,
    }

}
