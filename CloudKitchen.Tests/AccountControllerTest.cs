using CloudKitchen.Controllers;
using DataLibrary.Helpers;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CloudKitchen.Tests
{
    public class AccountControllerTest
    {
        private readonly AccountController _accountController;
        private readonly Mock<IUserDetailsHelper> _mockUserDetailsHelper;

        public AccountControllerTest()
        {
            _mockUserDetailsHelper = new Mock<IUserDetailsHelper>();
            _accountController = new AccountController(_mockUserDetailsHelper.Object);
            //_accountController = new AccountController(Mock.Of<IUserDetailsHelper>());
        }

        [Fact]
        public void Register_GivenUserDetails_ReturnsCreated()
        {
            //Arrange
            var userInputDetails = new UserInputDetails();

            //Act
            var result= _accountController.Register(userInputDetails) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            //Assert.IsType<ObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
            //Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Register_GivenUserIsPresent_ReturnBadRequest()
        {
            //Arrange
            var contactNo = "1234567890";
            var userInputDetails = new UserInputDetails()
            {
                ContactNo= contactNo
            };
            _mockUserDetailsHelper.Setup(x=>x.GetUserDetailsByContactNo(It.IsAny<string>())).Returns(new UserDetails());

            //Act
            var result=_accountController.Register(userInputDetails) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);

        }

        [Theory]
        [InlineData("1234567890",HttpStatusCode.BadRequest)]
        [InlineData("9876543210", HttpStatusCode.Created)]
        public void Register_GivenUserDetails_ReturnStatusCodeBasedOnUserAvailability(string contactNo, HttpStatusCode statusCode)
        {
            //Arrange
            var contactNoAlreadyPresent = "1234567890";
            var userInputDetails = new UserInputDetails()
            {
                ContactNo = contactNo
            };

            _mockUserDetailsHelper.Setup(x => x.GetUserDetailsByContactNo(contactNoAlreadyPresent)).Returns(new UserDetails());

            //Act
            var result = _accountController.Register(userInputDetails) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)statusCode, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(TestDataShared.UserTestDataExternal),MemberType =typeof(TestDataShared))]
        public void Register_GivenUserDetailsFromSharedData_ReturnStatusCodeBasedOnUserAvailability(string contactNo, HttpStatusCode statusCode)
        {
            //Arrange
            var contactNoAlreadyPresent = "1234567890";
            var userInputDetails = new UserInputDetails()
            {
                ContactNo = contactNo
            };

            _mockUserDetailsHelper.Setup(x => x.GetUserDetailsByContactNo(contactNoAlreadyPresent)).Returns(new UserDetails());

            //Act
            var result = _accountController.Register(userInputDetails) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)statusCode, result.StatusCode);
        }

        [Theory]
        [TestDataShared]
        public void Register_GivenUserDetailsFromDataAttribute_ReturnStatusCodeBasedOnUserAvailability(string contactNo, HttpStatusCode statusCode)
        {
            //Arrange
            var contactNoAlreadyPresent = "1234567890";
            var userInputDetails = new UserInputDetails()
            {
                ContactNo = contactNo
            };

            _mockUserDetailsHelper.Setup(x => x.GetUserDetailsByContactNo(contactNoAlreadyPresent)).Returns(new UserDetails());

            //Act
            var result = _accountController.Register(userInputDetails) as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal((int)statusCode, result.StatusCode);
        }
    }
}
