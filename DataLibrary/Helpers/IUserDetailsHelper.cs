using DataLibrary.Models;

namespace DataLibrary.Helpers
{
    public interface IUserDetailsHelper
    {
        int CreateUser(UserInputDetails userInputDetails);
        UserDetails GetUserDetailsByContactNo(string contactNo);
        UserDetails GetUserDetailsByContactNoAndPassword(string contactNo, string password);
    }
}