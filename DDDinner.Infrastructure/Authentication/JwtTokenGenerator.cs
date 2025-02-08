using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Application.Common.Services;
using DDDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(User user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.UniqueName, Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials:signingCredentials,
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires:_dateTimeProvider.UtcNow.AddDays(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
            
        }
    }
}
