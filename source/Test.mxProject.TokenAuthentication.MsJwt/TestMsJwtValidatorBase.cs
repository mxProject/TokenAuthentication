using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mxProject.TokenAuthentication;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class TestMsJwtValidatorBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract MsJwtProviderBase<TestPayload> GetProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        public abstract MsJwtValidatorBase<TestPayload> CreateValidator(string issuer, string audience);

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken()
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = TestMsJwtProviderBase.CreatePayload();

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(TestConstants.Issuer, TestConstants.Audience);

            Assert.IsTrue(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));

            Assert.IsTrue(ValueEquals(claim, tokenClaim));
            Assert.IsTrue(ValueEquals(payload, tokenPayload));
            Assert.AreEqual(state, TokenState.Valid);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_NullPayload()
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = null;

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(TestConstants.Issuer, TestConstants.Audience);

            Assert.IsTrue(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));

            Assert.IsTrue(ValueEquals(claim, tokenClaim));
            Assert.IsNull(tokenPayload);
            Assert.AreEqual(state, TokenState.Valid);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_Expired()
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = TestMsJwtProviderBase.CreatePayload();

            claim.Expiration = DateTimeOffset.UtcNow.AddSeconds(-1);

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(TestConstants.Issuer, TestConstants.Audience);

            Assert.IsFalse(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));

            Assert.AreEqual(state, TokenState.Expired);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_NotBefore()
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = TestMsJwtProviderBase.CreatePayload();

            claim.NotBefore = DateTimeOffset.UtcNow.AddSeconds(5);

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(TestConstants.Issuer, TestConstants.Audience);

            Assert.IsFalse(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));

            Assert.AreEqual(state, TokenState.NotBefore);

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_InvalidIssuer()
        {
            CreateToken_InvalidIssuer(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_InvalidIssuer_NotValidate()
        {
            CreateToken_InvalidIssuer(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateToken_InvalidIssuer(bool validateIssuer)
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = TestMsJwtProviderBase.CreatePayload();

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(validateIssuer ? "thisIssuer" : null, TestConstants.Audience);

            if (validateIssuer)
            {
                Assert.IsFalse(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));
                Assert.AreEqual(state, TokenState.InvalidIssuer);
            }
            else
            {
                Assert.IsTrue(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));
                Assert.AreEqual(state, TokenState.Valid);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_InvalidAudience()
        {
            CreateToken_InvalidAudience(true);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateToken_InvalidAudience_NotValidate()
        {
            CreateToken_InvalidAudience(false);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateToken_InvalidAudience(bool validateAudience)
        {

            TokenClaim claim = TestMsJwtProviderBase.CreateClaim();
            TestPayload payload = TestMsJwtProviderBase.CreatePayload();

            string token = GetProvider().CreateToken(claim, payload);

            MsJwtValidatorBase<TestPayload> validator = CreateValidator(TestConstants.Issuer, validateAudience ? "thisAudience" : null);

            if (validateAudience)
            {
                Assert.IsFalse(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));
                Assert.AreEqual(state, TokenState.InvalidAudience);
            }
            else
            {
                Assert.IsTrue(validator.ValidateToken(token, out ITokenClaim tokenClaim, out TestPayload tokenPayload, out TokenState state, out string errorMessage));
                Assert.AreEqual(state, TokenState.Valid);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool ValueEquals(ITokenClaim a, ITokenClaim b)
        {
            if (a.JwtID != b.JwtID) { return false; }
            if (a.Audience != b.Audience) { return false; }
            if (a.Expiration != b.Expiration) { return false; }
            if (a.NotBefore != b.NotBefore) { return false; }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private bool ValueEquals(TestPayload a, TestPayload b)
        {
            if (a.IntValue != b.IntValue) { return false; }
            if (a.StringValue != b.StringValue) { return false; }

            if ((a.Items == null) != (b.Items == null)) { return false; }

            if (a.Items != null)
            {
                if (a.Items.Count != b.Items.Count) { return false; }

                for (int i = 0; i < a.Items.Count; ++i)
                {
                    if (a.Items[i].Value != b.Items[i].Value) { return false; }
                }
            }

            return true;
        }
        
    }
}
