using DDDinner.Contracts.Dtos;
using DDDinner.Domain.Entities;

namespace DDDinner.Application.Common.Interfaces.Authentication
{
    public interface IPasswordUtility
    {
        string GenerateToken(User user);
        bool CheckHash(string password, byte[] passwordHash, byte[] passwordSalt);
        PasswordHashDto CreatePasswordDto(string password);
    }
}
