using DDDinner.Application.Common.Persistence;
using DDDinner.Domain.Entities;

namespace DDDinner.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User GetUserByEmail(string email)
        {
            var user = _users.SingleOrDefault(x => x.Email == email);
            return user;
        }
    }
}
