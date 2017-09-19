using Box.V2.Models;
using BoxGraphQLWrapper.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Microsoft.Extensions.Caching.Memory;
using BoxGraphQLWrapper.Backend.Caching;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper.Backend
{
    public class FolderService : IFolderService
    {
        public FolderService(IMemoryCache cache, IClientService clientService, ILogger<FolderService> log)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized.");
            Cache = cache ?? throw new ArgumentNullException(nameof(cache), $"{nameof(IMemoryCache)} not initialized.");
            Log = log ?? throw new ArgumentNullException(nameof(log), $"{nameof(ILogger<ItemService>)} not initialized.");
        }

        private IClientService ClientService { get; }
        private IMemoryCache Cache { get; }
        private ILogger<FolderService> Log { get; }

        public async Task<BoxFolder> GetFolderById(string id)
        {
            BoxClient client = ClientService.GetClient();

            BoxFolder folder = null;
            if(id != null)
            {
                CacheKey<BoxFolder> key = new CacheKey<BoxFolder>(client.Auth.Session.AccessToken, id);
                folder = await Cache.GetOrCreateAsync(key, cacheEntry =>
                {
                    Log.LogTrace($"Caching folder {id}.");

                    cacheEntry.SetSlidingExpiration(TimeSpan.FromSeconds(20));
                    return client.FoldersManager.GetInformationAsync(id);
                }).ConfigureAwait(false);
            }

            return folder;
        }
    }
}
