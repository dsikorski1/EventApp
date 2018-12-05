using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Extensions
{
    public static class RepositoryExtension
    {
        public static async Task<Event> GetOrFailAsync(this IEventRepository repository, Guid id)
        {
            var @event = await repository.GetAsync(id);
            if(@event == null)
            {
                throw new Exception($"Event with id: '{id}' does not exists.");
            }

            return @event;
        }

        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid id)
        {
            var user = await repository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"Event with id: '{id}' does not exists.");
            }

            return user;
        }
    }
}
