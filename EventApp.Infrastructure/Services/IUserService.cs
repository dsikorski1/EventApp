using EventApp.Core.Domain;
using EventApp.Infrastructure.Commands.Users;
using System;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task<User> GetAsync(Guid id);
        Task<User> LoginAsync(LoginUser command);
        Task RegisterAsync(RegisterUser command);
    }
}
