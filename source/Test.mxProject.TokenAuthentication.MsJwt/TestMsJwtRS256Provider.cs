using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMsJwtRS256Provider : TestMsJwtProviderBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MsJwtProviderBase<TestPayload> CreateProvider()
        {
            MsJwtRs256Provider<TestPayload> provider = MsJwtFactory.CreateRs256Provider<TestPayload>(TestConstants.RS256PrivateKey, TestConstants.Issuer);

            return provider;
        }

    }
}
