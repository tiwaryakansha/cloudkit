using DataLibrary.Context;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Helpers
{
    public class UserDetailsHelper : IUserDetailsHelper
    {
        private readonly ApplicationContext _dbContext;
        public UserDetailsHelper(ApplicationContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public UserDetails GetUserDetailsByContactNo(string contactNo)
        {
            return _dbContext.Users.Where(x => x.ContactNo == contactNo).FirstOrDefault();
        }
        public UserDetails GetUserDetailsByContactNoAndPassword(string contactNo, string password)
        {
            return _dbContext.Users.Where(x => x.ContactNo == contactNo && x.Password == password).FirstOrDefault();
        }

        public Int32 CreateUser(UserInputDetails userInputDetails)
        {
            UserDetails userDetails = new()
            {
                Firstname = userInputDetails.Firstname,
                Lastname = userInputDetails.Lastname,
                Email = userInputDetails.Email,
                Password = userInputDetails.Password,
                Address = userInputDetails.Address,
                Role = userInputDetails.Role,
                ContactNo = userInputDetails.ContactNo

            };

            _dbContext.Users.Add(userDetails);
            _dbContext.SaveChanges();

            return userDetails.Id;
        }
    }
}
