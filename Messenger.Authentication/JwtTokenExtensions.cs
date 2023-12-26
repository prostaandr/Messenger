using Messenger.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Messenger.Authentication
{
    public static class JwtTokenExtensions
    {
        public static string GenerateJwtToken(this User user, TokenParameters tokenParameters)
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.Name, user.Id.ToString())
            }, "auth");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(tokenParameters.Issuer, tokenParameters.Audience, claimsIdentity.Claims, null, signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
