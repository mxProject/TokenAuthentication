# mxProject.TokenAuthentication

トークン認証インターフェースライブラリ。
mxProject.TokenAuthentication にはトークンの生成や検証に関する実装は含んでいません。
mxProject.TokenAuthentication.MsJwt は System.IdentityModel.Tokens.Jwt を使用した実装の例です。

[English page](README.md)

## 機能

* JWT クレームと任意の型のペイロードを含むトークンを生成します。
* トークンを検証し、トークンからクレームとペイロードを取得します。
* トークンの有効期限を管理し、トークンをリフレッシュします。

## 依存バージョン

* .NET Framework >= 4.6
* .NET Standard >= 2.0

### mxProject.TokenAuthentication.MsJwt
* System.IdentityModel.Tokens.Jwt >= 5.4.0
* Newtonsoft.Json >= 10.0.0

## ライセンス

[MIT Licence](http://opensource.org/licenses/mit-license.php)

## NuGet

[mxProject.TokenAuthentication](https://www.nuget.org/packages/mxProject.TokenAuthentication)
[mxProject.TokenAuthentication.MsJwt](https://www.nuget.org/packages/mxProject.TokenAuthentication.MsJwt)

## 使用方法

以降のサンプルコードで使用している TestPayload クラスは、次のような POCO クラスです。

```C#
public class TestPayload
{
    public IntValue { get; set; }
    public StringValue { get; set; }
}
```

### トークンの生成

プロバイダを作成し、プロバイダの CreateToken メソッドを呼び出します。

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

RS256を使用する場合は、次の方法でプロバイダを生成します。

```C#
// create the provider.
string RS256PrivateKey = "<RSAKeyValue><Modulus>yT12/iqZ ... mLenuDgQ==</D></RSAKeyValue>";
string issuer = "testIssuer";
ITokenProvider<TestPayload> provider
    = MsJwtFactory.CreateRs256Provider<TestPayload>(RS256PrivateKey, issuer);
```

### トークンの検証

バリデータを作成し、バリデータの ValidateToken メソッドを呼び出します。検証結果と共にクレームとペイロードを取得できます。

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

RS256を使用する場合は、次の方法でバリデータを生成します。

```C#
// create the validator.
string RS256PublicKey = "<RSAKeyValue><Modulus>yT12/iqZ ... XrBw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
string issuer = "testIssuer";
string audience = "testAudience";
ITokenValidator<TestPayload> validator
    = MsJwtFactory.CreateHs256Validator<TestPayload>(RS256PublicKey, issuer, audience);
```

#### TokenState 列挙体

|値|説明|
|:--|:--|
|Unknown|不明|
|Valid|有効|
|Invalid|何らかの理由により無効|
|InvalidIssuer|発行者が不正|
|InvalidAudience|利用者が不正|
|NotBefore|まだ有効になっていない|
|Expired|有効期限切れ|

### トークンのリフレッシュ

TokenManager クラスはアクセストークンとリフレッシュトークンを保持し、トークンの有効期限を管理します。

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
