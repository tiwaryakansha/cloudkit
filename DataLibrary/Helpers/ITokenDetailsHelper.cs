using DataLibrary.Models;

namespace DataLibrary.Helpers
{
    public interface ITokenDetailsHelper
    {
        int CreateToken(TokenModel model, UserDetails user);
        List<TokenModel> GetTokensByUserIdAndRefreshToken(int userId, string refreshToken);
        int RemoveOldAndCreateNew(TokenModel oldToken, TokenModel newToken);
    }
}