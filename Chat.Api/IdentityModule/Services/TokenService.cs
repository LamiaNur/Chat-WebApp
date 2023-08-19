using System.Composition;
using System.Security.Claims;
using System.Text;
using Chat.Api.IdentityModule.Constants;
using Chat.Api.IdentityModule.Helpers;
using Chat.Api.IdentityModule.Interfaces;
using Chat.Api.IdentityModule.Models;
using Chat.Framework.Services;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Api.IdentityModule.Services
{
    [Export(typeof(ITokenService))]
    [Shared]
    public class TokenService : ITokenService
    {
        private readonly TokenConfig _tokenConfig;
        private readonly IAccessRepository _accessRepository;

        public TokenService()
        {
            _tokenConfig = DIService.Instance.GetConfiguration().GetSection("TokenConfig").Get<TokenConfig>();
            _accessRepository = DIService.Instance.GetService<IAccessRepository>();
        }
        
        public async Task<Token?> CreateTokenAsync(UserProfile userProfile, string appId)
        {
            var accessModel = GenerateAccessModel(userProfile, appId);
            var isSave = await _accessRepository.SaveAccessModelAsync(accessModel);
            if (isSave == false) return null;
            return accessModel.ToToken();
        }

        public async Task<bool> RevokeAllTokenByAppIdAsync(string appId)
        {
            return await _accessRepository.DeleteAllTokenByAppId(appId);
        }

        public async Task<bool> RevokeAllTokensByUserId(string userId)
        {
            return await _accessRepository.DeleteAllTokensByUserId(userId);
        }

        public async Task<bool> SaveAccessModelAsync(AccessModel accessModel)
        {
            return await _accessRepository.SaveAccessModelAsync(accessModel);
        }
        public async Task<AccessModel?> GetAccessModelByRefreshTokenAsync(string refreshToken)
        {
            return await _accessRepository.GetAccessModelByIdAsync(refreshToken);
        }

        public AccessModel GenerateAccessModel(UserProfile userProfile, string appId)
        {
            var claims = GenerateClaims(userProfile, appId);
            var accessToken = TokenHelper.GenerateJwtToken(_tokenConfig.SecretKey, _tokenConfig.Issuer, _tokenConfig.Audience, _tokenConfig.ExpirationTimeInSec, claims);
            var refreshToken = TokenHelper.GenerateRefreshToken();
            var accessModel = new AccessModel
            {
                Id = refreshToken,
                AccessToken = accessToken,
                AppId = appId,
                UserId = userProfile.Id,
                Expired = false,
                CreatedAt = DateTime.UtcNow
            };
            return accessModel;
        }

        public List<Claim> GenerateClaims(UserProfile userProfile, string appId)
        {
            var claims = new List<Claim>
            {
                new Claim(UserClaims.UserId, userProfile.Id),
                new Claim(UserClaims.ProfilePictureId, userProfile.ProfilePictureId),
                new Claim(UserClaims.Email, userProfile.Email),
                new Claim(UserClaims.FirstName, userProfile.FirstName),
                new Claim(UserClaims.LastName, userProfile.LastName),
                new Claim(UserClaims.UserName, userProfile.UserName),
                new Claim(UserClaims.AppId, appId),
                new Claim(UserClaims.JwtId, Guid.NewGuid().ToString())
            };

            return claims;
        }

        public UserProfile GetUserProfileFromAccessToken(string? accessToken)
        {
            var claims = TokenHelper.GetClaims(accessToken);
            var userProfile = new UserProfile();
            foreach (var claim in claims)
            {
                if (claim.Type == UserClaims.UserId) 
                {
                    userProfile.Id = claim.Value;
                }
                else if (claim.Type == UserClaims.FirstName) 
                {
                    userProfile.FirstName = claim.Value;
                }
                else if (claim.Type == UserClaims.LastName) 
                {
                    userProfile.LastName = claim.Value;
                }
                else if (claim.Type == UserClaims.UserName) 
                {
                    userProfile.UserName = claim.Value;
                }
                else if (claim.Type == UserClaims.Email) 
                {
                    userProfile.Email = claim.Value;
                }
                else if (claim.Type == UserClaims.ProfilePictureId)
                {
                    userProfile.ProfilePictureId = claim.Value;
                }
            }
            return userProfile;
        }

        public TokenValidationParameters GetTokenValidationParameters(bool validateIssuer = true, bool validateAudience = true, bool validateLifetime = true, bool validateIssuerSigningKey = true)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = validateIssuer,
                ValidateAudience = validateAudience,
                ValidateLifetime = validateLifetime,
                ValidateIssuerSigningKey = validateIssuerSigningKey,

                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _tokenConfig.Issuer,
                ValidAudience = _tokenConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_tokenConfig.SecretKey))
            };
        }
    }
}