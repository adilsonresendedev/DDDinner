using DDDinner.Domain.Entities;

namespace DDDinner.Application.Common.Persistence
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}
