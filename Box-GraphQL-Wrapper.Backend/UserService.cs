using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Models;
using BoxGraphQLWrapper.Backend.Caching;
using BoxGraphQLWrapper.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper.Backend
{
    public class UserService : IUserService
    {
        public UserService(IMemoryCache cache, IClientService clientService, ILogger<UserService> log)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized.");
            Cache = cache ?? throw new ArgumentNullException(nameof(cache), $"{nameof(IMemoryCache)} not initialized.");
            Log = log ?? throw new ArgumentNullException(nameof(log), $"{nameof(ILogger<ItemService>)} not initialized.");
        }

        private IClientService ClientService { get; }
        private IMemoryCache Cache { get; }
        private ILogger<UserService> Log { get; }

        public async Task<BoxUser> GetUserById(string id)
        {
            BoxClient client = ClientService.GetClient();
            
            BoxUser user = null;
            if(id != null)
            {
                CacheKey<BoxUser> key = new CacheKey<BoxUser>(client.Auth.Session.AccessToken, id);
                user = await Cache.GetOrCreateAsync(key, cacheEntry =>
                {
                    Log.LogTrace($"Caching user {id}.");

                    cacheEntry.SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    return client.UsersManager.GetUserInformationAsync(id);
                }).ConfigureAwait(false);
            }

            return user;
        }
    }
}
