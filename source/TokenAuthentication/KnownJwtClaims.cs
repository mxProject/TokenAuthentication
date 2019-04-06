using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Well-known claims of JWT.
    /// </summary>
    public sealed class KnownJwtClaims
    {

        /// <summary>
        /// iss : Issuer
        /// </summary>
        public static readonly string Issuer = "iss";

        /// <summary>
        /// sub : Subject
        /// </summary>
        public static readonly string Subject = "sub";

        /// <summary>
        /// aud : Audience
        /// </summary>
        public static readonly string Audience = "aud";

        /// <summary>
        /// exp : Expiration
        /// </summary>
        public static readonly string Expiration = "exp";

        /// <summary>
        /// nbf : NotBefore
        /// </summary>
        public static readonly string NotBefore = "nbf";

        /// <summary>
        /// iat : IssuedAt
        /// </summary>
        public static readonly string IssuedAt = "iat";

        /// <summary>
        /// jti : JWT ID
        /// </summary>
        public static readonly string JwtID = "jti";

        /// <summary>
        /// typ : Type
        /// </summary>
        public static readonly string Type = "typ";

        /// <summary>
        /// payload : Payload object (Not officially reserved)
        /// </summary>
        public static readonly string UserPayload = "payload";

    }

}
