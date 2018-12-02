using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.Commands.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
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
