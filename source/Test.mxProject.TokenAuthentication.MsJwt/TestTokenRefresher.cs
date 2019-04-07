using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    [TestClass]
    public class TestTokenRefresher
    {

        readonly MsJwtProviderBase<TestPayload> m_Provider = new TestMsJwtRS256Provider().CreateProvider();
        readonly MsJwtValidatorBase<TestPayload> m_Validator = new TestMsJwtRS256Validator().CreateValidator(TestConstants.Issuer, TestConstants.Audience);

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SetToken()
        {

            ITokenPair tokenPair = CreateTokenPair(TestMsJwtProviderBase.CreateClaim(), TestMsJwtProviderBase.CreatePayload());

            TokenManager refresher = CreateRefresher(TestConstants.AccessTokenLifetimeSeconds / 2);

            refresher.SetToken(tokenPair);

            Assert.IsFalse(refresher.NeedRefreshToken());
            
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SetToken_NeedRefresh()
        {

            ITokenPair tokenPair = CreateTokenPair(TestMsJwtProviderBase.CreateClaim(), TestMsJwtProviderBase.CreatePayload());

            TokenManager refresher = CreateRefresher(TestConstants.AccessTokenLifetimeSeconds);

            refresher.SetToken(tokenPair);

            Assert.IsTrue(refresher.NeedRefreshToken());

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RefreshToken()
        {

            ITokenPair tokenPair = CreateTokenPair(TestMsJwtProviderBase.CreateClaim(), TestMsJwtProviderBase.CreatePayload());

            TokenManager refresher = CreateRefresher(TestConstants.AccessTokenLifetimeSeconds / 2);

            refresher.SetToken(tokenPair);

            Assert.IsFalse(refresher.NeedRefreshToken());

            DateTimeOffset lastExpiration = refresher.GetTokenExpiration().Value;

            System.Threading.Thread.Sleep(1000);

            DateTimeOffset expectedExpiration = DateTimeOffset.UtcNow.AddSeconds(TestConstants.AccessTokenLifetimeSeconds);

            refresher.RefreshToken();

            Assert.IsTrue(lastExpiration < refresher.GetTokenExpiration().Value);
            Assert.IsTrue(expectedExpiration <= refresher.GetTokenExpiration().Value);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secondsBefore"></param>
        /// <returns></returns>
        private TokenManager CreateRefresher(int secondsBefore)
        {

            return new TokenManager(token =>
            {

                if (!m_Validator.ValidateToken(token, out ITokenClaim claim, out TestPayload payload, out TokenState state, out string errorMessage))
                {
                    throw new ArgumentException(errorMessage);
                }

                TokenClaim newClaim = new TokenClaim()
                {
                    JwtID = claim.JwtID,
                    Audience = claim.Audience,
                    Expiration = DateTimeOffset.UtcNow.AddSeconds(TestConstants.AccessTokenLifetimeSeconds),
                };

                return CreateTokenPair(newClaim, payload);

            }
            , secondsBefore
            );

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claim"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        private ITokenPair CreateTokenPair(TokenClaim claim, TestPayload payload)
        {

            TokenInfo accessToken = new TokenInfo(m_Provider.CreateToken(claim, payload), claim.Expiration, claim.NotBefore);

            claim.Expiration = DateTimeOffset.UtcNow.AddSeconds(TestConstants.RefreshTokenLifetimeSeconds);

            TokenInfo refreshToken = new TokenInfo(m_Provider.CreateToken(claim, payload), claim.Expiration, claim.NotBefore);

            return new TokenPair(accessToken, refreshToken);

        }

    }
}
