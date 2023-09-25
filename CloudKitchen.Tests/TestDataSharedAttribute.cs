using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace CloudKitchen.Tests
{
    public class TestDataSharedAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { "1234567890", HttpStatusCode.BadRequest };
            yield return new object[] { "9234567890", HttpStatusCode.Created };
        }
    }
}
