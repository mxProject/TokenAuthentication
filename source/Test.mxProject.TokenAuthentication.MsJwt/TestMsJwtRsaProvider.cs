using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMsJwtRsaProvider
    {

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken()
        {

            TokenClaim claim = CreateClaim();
            TestPayload payload = CreatePayload();

            CreateToken(claim, payload);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_Expired()
        {

            TokenClaim claim = CreateClaim();
            TestPayload payload = CreatePayload();

            claim.Expiration = DateTimeOffset.UtcNow.AddSeconds(-1);

            CreateToken(claim, payload);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateToken_NullClaim()
        {

            TokenClaim claim = null;
            TestPayload payload = CreatePayload();

            CreateToken(claim, payload);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_NullPayload()
        {

            TokenClaim claim = CreateClaim();
            TestPayload payload = null;

            claim.Expiration = DateTimeOffset.UtcNow.AddSeconds(-1);

            CreateToken(claim, payload);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static TokenClaim CreateClaim()
        {
            return new TokenClaim()
            {
                JwtID = Guid.NewGuid().ToString(),
                Audience = TestConstants.Audience,
                Expiration = DateTimeOffset.UtcNow.AddSeconds(TestConstants.AccessTokenLifetimeSeconds).TrancateMillisecond(),
                NotBefore = DateTimeOffset.UtcNow.AddSeconds(-5).TrancateMillisecond(),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static TestPayload CreatePayload()
        {
            return new TestPayload()
            {
                IntValue = 1,
                StringValue = "a",
                Items = new TestPayloadItem[] {
                    new TestPayloadItem() { Value="item1" },
                    new TestPayloadItem() { Value="item2" }
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claim"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        internal static string CreateToken(TokenClaim claim, TestPayload payload)
        {

            MsJwtRsaProvider<TestPayload> provider = CreateProvider();

            return provider.CreateToken(claim, payload);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static MsJwtRsaProvider<TestPayload> CreateProvider()
        {
            MsJwtRsaProvider<TestPayload> provider = MsJwtFactory.CreateRsaProvider<TestPayload>(TestConstants.RsaPrivateKey);

            provider.Issuer = TestConstants.Issuer;

            return provider;
        }

    }
}
