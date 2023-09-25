using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public  class TokenResponseModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Refresh_Token { get; set; }
        public string Roles { get; set; }
        public string Username { get; set; }
    }
}
