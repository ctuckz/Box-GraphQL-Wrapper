using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Models;
using BoxGraphQLWrapper.Interfaces;

namespace BoxGraphQLWrapper.Backend
{
    public class UserService : IUserService
    {
        public UserService(IClientService clientService)
        {
            ClientService = clientService ?? throw new ArgumentNullException(nameof(clientService), $"{nameof(IClientService)} not initialized.");
        }

        private IClientService ClientService { get; }

        public async Task<BoxUser> GetUserById(string id)
        {
            BoxClient client = ClientService.GetClient();

            BoxUser user = null;
            if(id != null)
            {
                user = await client.UsersManager.GetUserInformationAsync(id).ConfigureAwait(false);
            }

            return user;
        }
    }
}
