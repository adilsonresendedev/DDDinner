using DDDinner.Domain.Entities;

namespace DDDinner.Application.Common.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task Add(User user);
    }
}
