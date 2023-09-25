using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudKitchen.Tests
{
    public static class TestDataShared
    {
        public static IEnumerable<object[]> UserTestData 
        {
            get
            {
                yield return new object[] {"1234567890", HttpStatusCode.BadRequest };
                yield return new object[] { "9234567890", HttpStatusCode.Created };
            }
        
        
        }

        public static IEnumerable<object[]> UserTestDataExternal
        {
            get
            {
                var userData = File.ReadAllLines("UserSharedData.txt");
                return userData.Select(x =>
                {
                    var linesplit = x.Split(",");
                    HttpStatusCode statusCode = HttpStatusCode.Created;

                    var userPhone = linesplit[0];
                    var userStatusCode = int.Parse(linesplit[1]);

                    if (userStatusCode == 400)
                    {
                        statusCode = HttpStatusCode.BadRequest;
                    }

                    return new object[] { userPhone, statusCode };
                });
            }


        }
    }
}
