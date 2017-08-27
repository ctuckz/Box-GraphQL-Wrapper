using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Models;
using Box_GraphQL_Wrapper.Interfaces;

namespace BoxGraphQLWrapper.Backend
{
    public class ItemService : IItemService
    {
        public ItemService(IClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized");
        }

        private IClientService ClientService { get; }

        public async Task<List<BoxItem>> GetItems(string parentFolderId)
        {
            BoxClient client = ClientService.GetClient();

            BoxCollection<BoxItem> items = null;
            if(parentFolderId != null)
            {
                items = await client.FoldersManager.GetFolderItemsAsync(parentFolderId, 500, 
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
            }

            return items.Entries;
        }
    }
}
