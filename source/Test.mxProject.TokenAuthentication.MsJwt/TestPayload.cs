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
    public class TestPayload
    {

        /// <summary>
        /// 
        /// </summary>
        public int IntValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<TestPayloadItem> Items { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class TestPayloadItem
    {
        public string Value { get; set; }
    }

}
