using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMsJwtHS256Validator : TestMsJwtValidatorBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MsJwtProviderBase<TestPayload> GetProvider()
        {
            return m_Provider.CreateProvider();
        }

        private TestMsJwtHS256Provider m_Provider = new TestMsJwtHS256Provider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        public override MsJwtValidatorBase<TestPayload> CreateValidator(string issuer, string audience)
        {
            MsJwtHs256Validator<TestPayload> validator = MsJwtFactory.CreateHs256Validator<TestPayload>(TestConstants.HS256CommonKey, issuer, audience);

            return validator;
        }

    }
}
