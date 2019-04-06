using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.mxProject.TokenAuthentication.MsJwt
{
    public static class DateTimeOffsetExtensions
    {

        public static DateTimeOffset TrancateMillisecond(this DateTimeOffset value)
        {
            return new DateTimeOffset(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, 0, value.Offset);
        }

    }
}
