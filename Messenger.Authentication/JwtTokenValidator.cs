using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Messenger.Authentication
{
    public class JwtTokenValidator : ISecurityTokenValidator
    {
        private string issuer;
        private string audience;
        private string secretKey;

        public bool CanValidateToken { get; } = true;
        public int MaximumTokenSizeInBytes { get; set; } = int.MaxValue;

        public JwtTokenValidator(TokenParameters tokenParameters)
        {
            issuer = tokenParameters.Issuer;
            audience = tokenParameters.Audience;
            secretKey = tokenParameters.SecretKey;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var claimsPrincipal = handler.ValidateToken(securityToken, tokenValidationParameters, out validatedToken);
            return claimsPrincipal;
        }

        public bool CanReadToken(string securityToken) => true;
    }
}
