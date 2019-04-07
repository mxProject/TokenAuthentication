using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// HS256 Token validator.
    /// </summary>
    /// <typeparam name="TPayload">The type of the payload.</typeparam>
    public class MsJwtHs256Validator<TPayload> : MsJwtValidatorBase<TPayload>
    {

        #region ctor

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="publicKey">The HS256 public key.</param>
        public MsJwtHs256Validator(SymmetricSecurityKey publicKey) : base(publicKey)
        {
        }

        #endregion

    }

}
