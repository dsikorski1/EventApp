using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.Commands.Users;
using EventApp.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private IJwtHandler _jwtHendler;

        public UserService(IUserRepository repository, IJwtHandler jwtHandler)
        {
            _repository = repository;
            _jwtHendler = jwtHandler;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<TokenDto> LoginAsync(LoginUser command)
        {
            var user = await _repository.GetAsync(command.Email);
            if(user == null || (user != null && user.Password != command.Password))
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHendler.CreateToken(user.Id, user.Role);

            return new TokenDto(jwt.Token, jwt.Expires, user.Role);
        }

        public async Task RegisterAsync(RegisterUser command)
        {
            var user = await _repository.GetAsync(command.Email);
            if(user != null)
            {
                throw new Exception($"User with email: '{command.Email}' already exists");
            }

            user = new User(command.Id, command.Email, command.Firstname, command.Lastname, command.Password);
            await _repository.AddAsync(user);
        }
    }
}
