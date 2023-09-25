using DataLibrary.Context;
using DataLibrary.Helpers;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudKitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserDetailsHelper _userDetailsHelper;
        public AccountController(IUserDetailsHelper userDetailsHelper)
        {
            _userDetailsHelper = userDetailsHelper;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] UserInputDetails formData)
        {
            if (ModelState.IsValid)
            {
                var userDetails1 = _userDetailsHelper.GetUserDetailsByContactNo(formData.ContactNo);
                if(userDetails1 == null)
                {
                    Int32 userId = _userDetailsHelper.CreateUser(formData);
                    return Created("api/GetUserById/"+userId.ToString(), new {userId=userId}); //change 200 to 201
                }

                return BadRequest($"User with phone {formData.ContactNo} is already present");
            }


            return BadRequest("One or more validation errors");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetUserById([FromRoute] Int32 id)
        {
            return Ok(new UserDetails() { });
        }


    }
}
