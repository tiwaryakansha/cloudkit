﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class TokenRequestModel
    {
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public string ContactNo { get; set; }
        public string RefreshToken { get; set; }
        public string Password { get; set; }
    }
}
