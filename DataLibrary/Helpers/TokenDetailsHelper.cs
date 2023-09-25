using Common.Helpers;
using DataLibrary.Context;
using DataLibrary.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Helpers
{
    public class TokenDetailsHelper : ITokenDetailsHelper
    {
        private readonly ApplicationContext _dbContext;
        private readonly AppSettings _appSettings;
        public TokenDetailsHelper(ApplicationContext dbContext, IOptions<AppSettings> appsettings)
        {
            this._dbContext = dbContext;
            _appSettings = appsettings.Value;
        }

        public Int32 CreateToken(TokenModel model, UserDetails user)
        {
            var token = _dbContext.Tokens.Where(x => x.UserId == user.Id).FirstOrDefault();

            if (token != null)
            {
                _dbContext.Tokens.Remove(token);
            }
            _dbContext.Tokens.Add(model);
            _dbContext.SaveChanges();
            return model.Id;
        }

        public List<TokenModel> GetTokensByUserIdAndRefreshToken(int userId, string refreshToken) 
        {
            return (_dbContext.Tokens.Where(x => x.UserId == userId && x.Value == refreshToken).ToList());
        }

        public Int32 RemoveOldAndCreateNew(TokenModel oldToken,TokenModel newToken)
        {
            _dbContext.Tokens.Remove(oldToken);
            _dbContext.Tokens.Add(newToken);
            _dbContext.SaveChanges();
            return newToken.Id;
        }
    }
}



