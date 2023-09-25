using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Context
{
    public interface IDbContext
    {
        Guid SetUsers(UserDetails userDetails);
        UserDetails GetUserDetails(String loginId);
    }
}
