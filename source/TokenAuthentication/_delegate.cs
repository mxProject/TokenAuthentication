using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.TokenAuthentication
{

    /// <summary>
    /// Indicates that this is a method to refresh the specified token.
    /// </summary>
    /// <param name="tokenString">The token string.</param>
    /// <returns>Refreshed new token.</returns>
    public delegate ITokenPair RefreshTokenDelegate(string tokenString);

}
