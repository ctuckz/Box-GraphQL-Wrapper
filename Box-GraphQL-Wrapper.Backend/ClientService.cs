using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box_GraphQL_Wrapper.Interfaces;

namespace BoxGraphQLWrapper.Backend
{
    public class ClientService : IClientService
    {
        public ClientService(IAuthenticationConfiguration authConfig)
        {
            AuthConfig = authConfig 
                ?? throw new ArgumentNullException(nameof(authConfig), $"{nameof(IAuthenticationConfiguration)} was not configured.");

            BoxConfig config = new BoxConfig(AuthConfig.ClientId, AuthConfig.ClientSecret, new Uri("http://localhost"));
            OAuthSession session = new OAuthSession(AuthConfig.DeveloperToken, "NOT_NEEDED", 3600, "bearer");
            Client = new BoxClient(config, session);
        }

        private IAuthenticationConfiguration AuthConfig { get; }
        private BoxClient Client { get; }

        public BoxClient GetClient()
        {
            return Client;
        }
    }
}
