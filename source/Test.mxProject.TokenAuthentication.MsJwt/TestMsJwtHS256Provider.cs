using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMsJwtHS256Provider : TestMsJwtProviderBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MsJwtProviderBase<TestPayload> CreateProvider()
        {
            MsJwtHs256Provider<TestPayload> provider = MsJwtFactory.CreateHs256Provider<TestPayload>(TestConstants.HS256CommonKey, TestConstants.Issuer);

            return provider;
        }

    }
}
