using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Api.CoreModule.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(string secretKey, string issuer, string audience, int expiredTimeInSec, List<Claim>? claims = null)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secretKey));
                var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var expiredTime = DateTime.Now.AddSeconds(expiredTimeInSec);
                var tokenOptions = new JwtSecurityToken(
                    issuer : issuer, 
                    audience : audience, 
                    claims : claims, 
                    expires : expiredTime, 
                    signingCredentials: signingCredentials
                );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
            
        }

        public static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
        
        public static List<Claim> GetClaims(string? accessToken)
        {
            try
            {
                accessToken = GetPreparedToken(accessToken);
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                return jwtSecurityToken.Claims.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Claim>();
            }
        }
        
        public static string GetClaimType(Claim claim)
        {
            return claim.Type;
        }
        
        public static string GetClaimValue(Claim claim)
        {
            return claim.Value;
        }

        public static bool IsTokenValid(string? accessToken, TokenValidationParameters validateParameters)
        {
            try
            {
                accessToken = GetPreparedToken(accessToken);
                new JwtSecurityTokenHandler().ValidateToken(accessToken, validateParameters, out var validatedToken);
                Console.WriteLine($"Token Valid : {accessToken}\n");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token InValid : {accessToken}\n{ex.Message}\n");
                return false;
            }
        }

        public static bool IsExpired(string? accessToken)
        {
            try
            {
                accessToken = GetPreparedToken(accessToken);
                var securityToken = new JwtSecurityToken(accessToken);
                bool isExpired = securityToken.ValidTo < DateTime.UtcNow;
                string message = "Not Expired";
                if (isExpired) message = "Expired";
                Console.WriteLine($"Token {message}. Token : {accessToken}\n");
                return isExpired;
            }
            catch (Exception)
            {
                Console.WriteLine($"Token error : {accessToken}\n");
                return false;
            }
        }
        private static string? GetPreparedToken(string? accessToken)
        {
            if (string.IsNullOrEmpty(accessToken) == false && accessToken.StartsWith("Bearer "))
            {
                return accessToken.Replace("Bearer ", "");
            }
            return accessToken;
        }   
    }
}