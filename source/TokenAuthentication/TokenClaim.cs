using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Claim of the token.
    /// </summary>
    public class TokenClaim : ITokenClaim
    {

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public string JwtID { get; set; }

        /// <summary>
        /// Gets or sets the audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets the expiration time.
        /// </summary>
        public DateTimeOffset Expiration { get; set; }

        /// <summary>
        /// Gets the notbefore time.
        /// </summary>
        public DateTimeOffset NotBefore { get; set; } = DateTimeOffset.UtcNow.AddSeconds(-5).UtcDateTime;

    }

}
