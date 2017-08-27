using Box.V2.Models;
using Box_GraphQL_Wrapper.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;

namespace BoxGraphQLWrapper.Backend
{
    public class FolderService : IFolderService
    {
        public FolderService(IClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized.");
        }

        private IClientService ClientService { get; }

        public async Task<BoxFolder> GetFolderById(string id)
        {
            BoxClient client = ClientService.GetClient();

            BoxFolder folder = null;
            if(id != null)
            {
                folder = await client.FoldersManager.GetInformationAsync(id);
            }

            return folder;
        }
    }
}
