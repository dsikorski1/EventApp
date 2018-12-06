using EventApp.Infrastructure.Commands.Users;
using EventApp.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAsync(Guid id);
        Task<TokenDto> LoginAsync(LoginUser command);
        Task RegisterAsync(RegisterUser command);
    }
}
