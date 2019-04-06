using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.mxProject.TokenAuthentication.MsJwt
{

    /// <summary>
    /// 
    /// </summary>
    static class TestConstants
    {

        /// <summary>
        /// 
        /// </summary>
        internal readonly static string RsaPrivateKey = "<RSAKeyValue><Modulus>yT12/iqZLNcrnTTFGy3NMuCjo6wJNLuG5j5L2yM6iX7CT5sWVq2BuXtdbq6PFuOIkzwJ+5Sng+qthAX5qHnuxRMI+QITe1qP+k0pOtK/EVtuedz6zdu2+Sp24CvGIMt1y8yMeOBXrRZTZzxpH9VsSq9kA/ylHKuWRfWLHysIqsdO0Tgf9eLwNAhRr6vpkvsAwvJnreIdWr/7aTrt9vq3EIJI3NYHV7/zqbZ7mKS1GbvJkAMbrQkYJ45hhEBUdYE45V8Dhkb9NTlExIcrar3vqsXSOVjQvuiGN4HsYmqPGUw26P9F7DrPyM4eQksb+PRMdkPW4dTjIRj9X3OIBHXrBw==</Modulus><Exponent>AQAB</Exponent><P>8Qw9p6A+11Tu6Dsl6+ndb7qiQP3u4cE5JMDRuq71A11XiEKU9K+1j5O26TtcJaJUCeH01RCKvMa/hNp2G7NqPnjxpRQU06Vj+bvJono7YTHcScC4Apa8cSsFQ62Iu2jpoHIkEz/5j7EdkToyFpC4opxbcHANPc9lXwfjIJTyieE=</P><Q>1bkXNBVazXVSGaP2DXVSSme9uXF5DmiEdKbpqRY6hlW+wIUBOG3RStkPC5ah62+3ObAooehVveR+kJOmSl2qLYvSaqV/DPkTyRyFOpTlpOSpLBsRvzPMoA7BFweXiy3YIbDsSr7S1qC1JgoMK4Htz742tDXLBUM32SWZr9OFoec=</Q><DP>aE8rvwYRK42NdOFjn5ssP9U7sXQxk2/SEp1+JJLhY/tYjZaCbwA6SU9ar8MINSDxzPUCxdDKuLYo2ozO313cc/xSVWVDPfMsOD2TG8RZPc4dzayf9D7WfQJo3MiTisXzk4LRKaNdk1jJura8RheKTpPq3dUfZcgBzgXTu5249wE=</DP><DQ>E8JP9d2/jl05YOt6tRXSrNRYgwuNoJpjHJHN6ncGpCLLRutFCJ2Giv/0VyLvB2BFtUynBQkA3FSCqwUri5aLRDi4FGoGjAF/JcnAO4FGle8aANzj0CSO14FlsqZeCV0MrVi5D9QClBs5hDHLnD4f6WPxlMmgYnUrdaT3R30rzqM=</DQ><InverseQ>dSfitpkpXxGrKbPA4HxVtSZU71tWOMbvIjYKy8cYTw+/EsQ7LW84Q1I8WDrbB7m/Zj67EufC2n1VNaP+x9dOCXpud+R/48piD2bp5JDCv5wUSs7xsjPsx8o1ScrHaXOeySQ486HTLji4RaqiiD1I46fF6NV1ZKRmOSUmMInxDDM=</InverseQ><D>DqjBkEY+HjwWWz9K1G4Dsp8WjIetq/+1FfSXxgDM9NMdCHt9pxbAimhoJ/XjSoGMo10ORRtREJT5ytI8m382W3jFgI4cKTIxpsQUKsrLTFJiu9HTG0fUDlZ/jljh9+WaURw3Z17AREWKEc0ew0jiuJYKLRgsVuhQ7Au09LJH0VjOTj9h62Trb2srbz/s+XjnTi8cch6oSBeqV/2YbYQla9bAMswR84fRRNUonDPrYvwC5rnhw5Xp0vJueHZpmTsruXjQJasue/Tgp/p6CsZlZX1CvTX8muSROyJ8vCjbG1dGplx+3Jbca+RoXj1FajdlmfrZxvDiH+v4M2mLenuDgQ==</D></RSAKeyValue>";

        /// <summary>
        /// 
        /// </summary>
        internal readonly static string RsaPublicKey = "<RSAKeyValue><Modulus>yT12/iqZLNcrnTTFGy3NMuCjo6wJNLuG5j5L2yM6iX7CT5sWVq2BuXtdbq6PFuOIkzwJ+5Sng+qthAX5qHnuxRMI+QITe1qP+k0pOtK/EVtuedz6zdu2+Sp24CvGIMt1y8yMeOBXrRZTZzxpH9VsSq9kA/ylHKuWRfWLHysIqsdO0Tgf9eLwNAhRr6vpkvsAwvJnreIdWr/7aTrt9vq3EIJI3NYHV7/zqbZ7mKS1GbvJkAMbrQkYJ45hhEBUdYE45V8Dhkb9NTlExIcrar3vqsXSOVjQvuiGN4HsYmqPGUw26P9F7DrPyM4eQksb+PRMdkPW4dTjIRj9X3OIBHXrBw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        /// <summary>
        /// 
        /// </summary>
        internal readonly static string Issuer = "testIssuer";

        /// <summary>
        /// 
        /// </summary>
        internal readonly static string Audience = "testAudience";

        /// <summary>
        /// 
        /// </summary>
        internal readonly static int AccessTokenLifetimeSeconds = 120;

        /// <summary>
        /// 
        /// </summary>
        internal readonly static int RefreshTokenLifetimeSeconds = 3600;

    }

}
