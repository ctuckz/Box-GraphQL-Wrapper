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
    public class ItemService : IItemService
    {
        public ItemService(IMemoryCache cache, IClientService clientService, ILogger<ItemService> log)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized");
            Cache = cache ?? throw new ArgumentNullException(nameof(cache), $"{nameof(IMemoryCache)} not initialized.");
            Log = log ?? throw new ArgumentNullException(nameof(log), $"{nameof(ILogger<ItemService>)} not initialized.");
        }

        private IClientService ClientService { get; }
        private IMemoryCache Cache { get; }
        private ILogger<ItemService> Log { get; }

        public async Task<List<BoxItem>> GetItems(string parentFolderId)
        {
            BoxClient client = ClientService.GetClient();

            BoxCollection<BoxItem> items = null;
            if(parentFolderId != null)
            {
                CacheKey<BoxItem> key = new CacheKey<BoxItem>(client.Auth.Session.AccessToken, parentFolderId);
                items = await Cache.GetOrCreateAsync(key, cacheEntry =>
                {
                    Log.LogTrace($"Caching items for folder {parentFolderId}.");

                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
                    return client.FoldersManager.GetFolderItemsAsync(parentFolderId, 500,
                    fields: new[] {
                        BoxItem.FieldDescription,
                        BoxItem.FieldName,
                        BoxItem.FieldSize,
                        BoxItem.FieldTags,
                        BoxItem.FieldCreatedAt,
                        BoxItem.FieldModifiedAt,
                        BoxItem.FieldParent,
                        BoxItem.FieldCreatedBy,
                        BoxItem.FieldModifiedBy,
                        BoxItem.FieldOwnedBy});
                }).ConfigureAwait(false);
            }

            return items.Entries;
        }
    }
}
