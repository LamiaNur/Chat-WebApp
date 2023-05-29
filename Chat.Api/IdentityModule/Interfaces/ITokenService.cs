using System.Security.Claims;
using Chat.Api.IdentityModule.Models;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Api.IdentityModule.Interfaces
{
    public interface ITokenService
    {
        AccessModel GenerateAccessModel(UserProfile userProfile, string appId);
        List<Claim> GenerateClaims(UserProfile userProfile, string appId);
        UserProfile GetUserProfileFromAccessToken(string accessToken);
        TokenValidationParameters GetTokenValidationParameters(bool validateIssuer = true, bool validateAudience = true, bool validateLifetime = true, bool validateIssuerSigningKey = true);
        Task<Token?> CreateTokenAsync(UserProfile userProfile, string appId);
        Task<bool> SaveAccessModelAsync(AccessModel accessModel);
        Task<bool> RevokeAllTokenByAppIdAsync(string appId);
        Task<bool> RevokeAllTokensByUserId(string userId);
        Task<AccessModel?> GetAccessModelByRefreshTokenAsync(string refreshToken);

    }
}