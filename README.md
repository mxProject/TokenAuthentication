# mxProject.TokenAuthentication

Token authentication interface.

mxProject.TokenAuthentication does not include an implementation for token generation and validation.
mxProject.TokenAuthentication.MsJwt is an example of implementation using System.IdentityModel.Tokens.Jwt.

[Japanese page](README.jp.md)

## Features

* Create a token containing the well-known JWT claims and type-safe payload.
* Validate tokens and retrieve claims and payload.
* Manage token expiration and refresh tokens.

## Requrements

* .NET Framework >= 4.6
* .NET Standard >= 2.0

### mxProject.TokenAuthentication.MsJwt
* System.IdentityModel.Tokens.Jwt >= 5.4.0
* Newtonsoft.Json >= 10.0.0

## Licence

[MIT Licence](http://opensource.org/licenses/mit-license.php)

## NuGet

[mxProject.TokenAuthentication](https://www.nuget.org/packages/mxProject.TokenAuthentication)
[mxProject.TokenAuthentication.MsJwt](https://www.nuget.org/packages/mxProject.TokenAuthentication.MsJwt)

## Usage

TestPayload class used in the following sample code is the following POCO class.

```C#
public class TestPayload
{
    public IntValue { get; set; }
    public StringValue { get; set; }
}
```

### Creates token

Create a provider and call CreateToken method.

```c#
// create the provider.
string HS256CommonKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
string issuer = "testIssuer";
ITokenProvider<TestPayload> provider
    = MsJwtFactory.CreateHs256Provider<TestPayload>(HS256CommonKey, issuer);

// create a claim and a payload.
ITokenClaim claim = new TokenClaim
{
    JwtID = Guid.NewGuid().ToString(),
    Audience = "testAudience",
    Expiration = DateTimeOffset.UtcNow.AddSeconds(120),
    NotBefore = DateTimeOffset.UtcNow.AddSeconds(-5),
};
TestPayload payload = new TestPayload
{
    IntValue = 1,
    StringValue = 'a',
};

// create a token.
string tokenString = provider.CreateToken(claim, payload);
```

When using RS256, create a provider in the following way.

```C#
// create the provider.
string RS256PrivateKey = "<RSAKeyValue><Modulus>yT12/iqZ ... mLenuDgQ==</D></RSAKeyValue>";
string issuer = "testIssuer";
ITokenProvider<TestPayload> provider
    = MsJwtFactory.CreateRs256Provider<TestPayload>(RS256PrivateKey, issuer);
```

### Validates token

Create a validator and call ValidateToken method.
You can get the claim and payload along with validation results.

```C#
// create the validator.
string HS256CommonKey = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
string issuer = "testIssuer";
string audience = "testAudience";
ITokenValidator<TestPayload> validator
    = MsJwtFactory.CreateHs256Validator<TestPayload>(HS256CommonKey, issuer, audience);

// validate the token.
bool valid = validator.ValidateToken(
    tokenString
    , out ITokenClaim tokenClaim
    , out TestPayload tokenPayload
    , out TokenState state
    , out string errorMessage
    );
```

When using RS256, create a validator in the following way.

```C#
// create the validator.
string RS256PublicKey = "<RSAKeyValue><Modulus>yT12/iqZ ... XrBw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
string issuer = "testIssuer";
string audience = "testAudience";
ITokenValidator<TestPayload> validator
    = MsJwtFactory.CreateHs256Validator<TestPayload>(RS256PublicKey, issuer, audience);
```

#### TokenState enum

|Value|Description|
|:--|:--|
|Unknown|Unknown.|
|Valid|The token is valid.|
|Invalid|The Token is invalid for some reason.|
|InvalidIssuer|The issuer is invalid.|
|InvalidAudience|The audience is invalid.|
|NotBefore|The token is not valid yet.|
|Expired|The token has expired.|

### Refreshes token

TokenManager class holds access token and refresh token, and manages token expiration.

```C#
private ITokenValidator<TestPayload> m_Validator;

/// <summary>
/// Test method for token refresh.
/// </summary>
[TestMethod]
public void RefreshToken()
{

    // create the token manager.
    int secondsBefore = 60;
    TokenManager manager = CreateManager(secondsBefore);

    // set a access token and a refresh token.
    // omit the implemetation of GetClaim method and GetPayload method.
    ITokenClaim claim = GetClaim();
    TestPayload payload = GetPayload();
    ITokenPair tokenPair = GetToken(claim, payload);
    manager.SetToken(tokenPair);

    // expect false.
    Assert.IsFalse(manager.NeedRefreshToken());

    // wait for the access token to expire...
    System.Threading.Thread.Sleep(1000);

    // expect true.
    Assert.IsTrue(manager.NeedRefreshToken());

    // refresh the token.
    manager.RefreshToken();

    // expect false.
    Assert.IsFalse(manager.NeedRefreshToken());

    // get the new token imformation.
    string newAccessToken = manager.GetAccessToken();
    DateTimeOffset? newAccessTokenExpiration = manager.GetTokenExpiration();

}

/// <summary>
/// Create a token manager.
/// </summary>
/// <param name="secondsBefore"></param>
/// <returns></returns>
private TokenManager CreateTokenManager(int secondsBefore)
{
    return new TokenManager(tokenString =>
    {
        // refresh the token

        if (!m_Validator.ValidateToken(
            tokenString
            , out ITokenClaim claim, out TestPayload payload
            , out TokenState state, out string errorMessage
            ))
        {
            throw new ArgumentException(errorMessage);
        }

        TokenClaim newClaim = new TokenClaim()
        {
            JwtID = claim.JwtID,
            Audience = claim.Audience,
            Expiration = DateTimeOffset.UtcNow.AddSeconds(120),
        };

        return GetToken(newClaim, payload);

    }
    , secondsBefore
    );
}

/// <summary>
/// Get a access token and a refresh token.
/// </summary>
/// <param name="claim"></param>
/// <param name="payload"></param>
/// <returns></returns>
private ITokenPair GetToken(TokenClaim claim, TestPayload payload)
{

    // get the token provider.(omit it's implemetation)
    ITokenProvider<TestPayload> provider = GetProvider();
    
    // create a access token.
    TokenInfo accessToken = new TokenInfo(
        provider.CreateToken(claim, payload)
        , claim.Expiration
        , claim.NotBefore
        );

    // create a refresh token.
    claim.Expiration = DateTimeOffset.UtcNow.AddSeconds(3600);
    TokenInfo refreshToken = new TokenInfo(
        provider.CreateToken(claim, payload)
        , claim.Expiration
        , claim.NotBefore
        );

    return new TokenPair(accessToken, refreshToken);
}
```
