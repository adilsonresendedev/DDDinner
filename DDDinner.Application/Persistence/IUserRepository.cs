using DDDinner.Domain.Entities;

namespace DDDinner.Application.Persistence
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task Add(User user);
    }
}
