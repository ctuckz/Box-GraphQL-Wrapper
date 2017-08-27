using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Box.V2.Config;
using Box.V2;
using Box.V2.Auth;
using BoxGraphQLWrapper.Configuration;
using Microsoft.Extensions.Options;
using Box.V2.Models;
using System.Net.Http;

namespace BoxGraphQLWrapper.Controllers
{
    [Produces("application/json")]
    [Route("api/GraphQL")]
    public class GraphQLController : Controller
    {
        private static HttpClient client = new HttpClient();

        public GraphQLController(IOptions<AuthenticationConfiguration> authConfig)
        {
            AuthConfig = authConfig.Value;
        }

        private AuthenticationConfiguration AuthConfig { get; }

        [HttpPost]
        public async Task<IEnumerable<string>> GraphQL()
        {
            List<string> ids = new List<string>();

            BoxConfig config = new BoxConfig(AuthConfig.ClientId, AuthConfig.ClientSecret, new Uri("http://localhost"));
            OAuthSession session = new OAuthSession(AuthConfig.DeveloperToken, "NOT_NEEDED", 3600, "bearer");
            BoxClient client = new BoxClient(config, session);

            try
            {
                BoxFile file = await client.FilesManager.GetInformationAsync("215999320939");
                return new[] { file.Id };
            }
            catch (Exception ex)
            {

            }
            return new[] { "error" };
        }
    }
}