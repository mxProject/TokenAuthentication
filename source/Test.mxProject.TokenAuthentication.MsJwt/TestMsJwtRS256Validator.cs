using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMsJwtRS256Validator : TestMsJwtValidatorBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override MsJwtProviderBase<TestPayload> GetProvider()
        {
            return m_Provider.CreateProvider();
        }

        private TestMsJwtRS256Provider m_Provider = new TestMsJwtRS256Provider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        public override MsJwtValidatorBase<TestPayload> CreateValidator(string issuer, string audience)
        {
            MsJwtRs256Validator<TestPayload> validator = MsJwtFactory.CreateRs256Validator<TestPayload>(TestConstants.RS256PublicKey, issuer, audience);

            return validator;
        }

    }
}
