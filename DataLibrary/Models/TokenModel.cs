using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TokenModel
    {
        [Key]
        public Int32 Id { get; set; }

        public string ClientId { get; set; }
        public string Value { get; set; }
        public DateTime CreateDate { get; set; }
        public Int32 UserId { get; set; }
        public DateTime LastMOdifiedDate { get; set; }
        public DateTime ExpiryTime { get; set; }
    }
}
