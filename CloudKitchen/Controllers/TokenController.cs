using Common.Helpers;
using DataLibrary.Context;
using DataLibrary.Helpers;
using DataLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CloudKitchen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserDetailsHelper _userDetailsHelper;
        private readonly ITokenDetailsHelper _tokenDetailsHelper;

        public TokenController(IUserDetailsHelper userDetailsHelper, IOptions<AppSettings> appSettings, ITokenDetailsHelper tokenDetailsHelper)
        {
            _userDetailsHelper = userDetailsHelper;
            _appSettings = appSettings.Value;
            _tokenDetailsHelper = tokenDetailsHelper;
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] TokenRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request details");
            }

            switch (model.GrantType)
            {

                case "password":
                    return GenerateNewToken(model);
                case "refresh_token":
                    return RefreshToken(model);
                default:
                    return new UnauthorizedResult();
            }

        }

        private IActionResult GenerateNewToken(TokenRequestModel model)
        {
            // check if tehre's an user with the given contactNO and password
            var user = _userDetailsHelper.GetUserDetailsByContactNoAndPassword(model.ContactNo, model.Password);
            
            if (user != null)
            {
                var newRtoken = CreateRefreshToken(_appSettings.ClientId, user.Id);
                var tokenId = _tokenDetailsHelper.CreateToken(newRtoken,user);
                if(tokenId > 0) 
                { 
                     var accessToken = CreateAccessToken(user, newRtoken.Value);
                     return Ok(new { authToken = accessToken });
                }
            }

            ModelState.AddModelError("", "User contactno/ password is not correct");
            return Unauthorized(new { LoginError = "Please check your login credentials" });
        }

        private TokenModel CreateRefreshToken(string clientId, int userId)
        {
            return new TokenModel()
            {                
                ClientId = clientId,
                UserId = userId,
                Value = Guid.NewGuid().ToString("N"),
                CreateDate = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(90)
            };

        }

        private TokenResponseModel CreateAccessToken(UserDetails user, string refreshToken)
        {
            double tokenExpiryTime = Convert.ToDouble(_appSettings.ExpireTime);

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));

            var roles = new List<String> { user.Role, "Customer" };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Firstname+" "+user.Lastname),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim("LoggedOn", DateTime.Now.ToString()),

                     }),

                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Site,
                Audience = _appSettings.Audience,
                Expires = DateTime.Now.AddMinutes(tokenExpiryTime)
            };

            // Generate token

            var newtoken = tokenHandler.CreateToken(tokenDescriptor);

            var encodedToken = tokenHandler.WriteToken(newtoken);

            return new TokenResponseModel()
            {
                Token = encodedToken,
                Expiration = newtoken.ValidTo,
                Refresh_Token = refreshToken,
                Roles = roles.FirstOrDefault(),
                Username = user.Firstname + " " + user.Lastname
            };
        }

        private IActionResult RefreshToken(TokenRequestModel model)
        {
            try
            {
                // check if the received refreshToken exists for the given clientId
                var user = _userDetailsHelper.GetUserDetailsByContactNo(model.ContactNo);
                if (user != null)
                {
                    var oldrTokens = _tokenDetailsHelper.GetTokensByUserIdAndRefreshToken(user.Id, model.RefreshToken);

                    if (oldrTokens.Count == 0)
                    {
                        // refresh token not found or invalid (or invalid clientId)
                        return new UnauthorizedResult();
                    }
                    
                    var rt = oldrTokens.FirstOrDefault();
                    // check if refresh token is expired
                    if (rt.ExpiryTime < DateTime.Now)
                    {
                        return new UnauthorizedResult();
                    }

                    // generate a new refresh token 

                    var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                    // invalidate the old refresh token (by deleting it)
                    // add the new refresh token
                    //if (rt != null)
                    //{
                    //    rtNew.Id = rt.Id;
                    //}

                    var tokenId = _tokenDetailsHelper.RemoveOldAndCreateNew(rt, rtNew);

                     // 

                     var response = CreateAccessToken(user, rtNew.Value);

                    return Ok(new { authToken = response });
                }
                ModelState.AddModelError("", "User contactno/refreshToken is not correct");
                return Unauthorized(new { LoginError = "Please check your login credentials" });               
            }
            catch (Exception ex)
            {

                return new UnauthorizedResult();
            }
        }
    }
}
