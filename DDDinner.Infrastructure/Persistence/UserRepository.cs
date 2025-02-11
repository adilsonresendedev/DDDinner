using DDDinner.Application.Persistence;
using DDDinner.Domain.Entities;

namespace DDDinner.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public async Task Add(User user)
        {
            await Task.Run(() =>
            {
                _users.Add(user);
            });
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = null;
            await Task<User>.Run(() =>
            {
                user = _users.SingleOrDefault(x => x.Email == email);
            });
            return user;
        }
    }
}
