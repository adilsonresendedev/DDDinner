using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Application.Common.Services;
using DDDinner.Contracts.Dtos;
using DDDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DDDinner.Infrastructure.Authentication
{
    public class PasswordUtility : IPasswordUtility
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public PasswordUtility(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings.Value;
        }

        public PasswordHashDto CreatePasswordDto(string password)
        {
            var passwordHashDto = new PasswordHashDto();
            using (var hmac = new HMACSHA512())
            {
                passwordHashDto.PasswordSalt = hmac.Key;
                passwordHashDto.PassworHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return passwordHashDto;
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

        public bool CheckHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmacsha = new HMACSHA512(passwordSalt))
            {
                var calculatedHash = hmacsha.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < calculatedHash.Length; i++)
                {
                    if (calculatedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
