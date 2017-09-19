using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using BoxGraphQLWrapper.Interfaces;

namespace BoxGraphQLWrapper.Backend
{
    public class ItemContentService : IItemContentService
    {
        public ItemContentService(IClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized");
        }

        private IClientService ClientService { get; }

        public async Task<string> GetItemDownloadUri(string itemId)
        {
            BoxClient client = ClientService.GetClient();

            if (itemId != null)
            {
                return (await client.FilesManager.GetDownloadUriAsync(itemId))?.ToString();
            }

            return null;
        }
    }
}
