using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly ISet<User> _users = new HashSet<User>
        {
            new User(Guid.NewGuid(), "dawid@test.com", "Dawid", "Sikorski", "test", "Admin"),
            new User(Guid.NewGuid(), "user@test.com", "User", "Evento", "test", "User")
        };

        public async Task<User> GetAsync(Guid id)
        {
            var result = _users.SingleOrDefault(u => u.Id == id);

            return await Task.FromResult(result);
        }

        public async Task<User> GetAsync(string email)
        {
            var result = _users.SingleOrDefault(u => u.Email.ToLowerInvariant() == email.ToLowerInvariant());

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<User>> BrowseAsync()
        {
            var result = _users.AsEnumerable();

            return await Task.FromResult(result);
        }

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }
    }
}
