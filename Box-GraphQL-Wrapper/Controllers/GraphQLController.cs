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
using GraphQL.Types;
using BoxGraphQLWrapper.GraphQL;
using GraphQL;
using Box_GraphQL_Wrapper.Interfaces;
using GraphQL.Http;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper.Controllers
{
    [Produces("application/json")]
    [Route("api/GraphQL")]
    public class GraphQLController : Controller
    {
        private static HttpClient client = new HttpClient();

        public GraphQLController(IFolderService folderService, IItemService itemService, ILogger<GraphQLController> logger)
        {
            FolderService = folderService ?? throw new ArgumentNullException(nameof(folderService), $"{nameof(IFolderService)} not configured.");
            ItemService = itemService ?? throw new ArgumentNullException(nameof(itemService), $"{nameof(IItemService)} not configured.");
            Logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(ILogger<GraphQLController>)} not configured.");
        }

        private IFolderService FolderService { get; }
        private IItemService ItemService { get; }
        private ILogger<GraphQLController> Logger { get; }

        [HttpPost]
        public async Task<string> GraphQL([FromBody] string query, [FromServices] IServiceProvider serviceProvider)
        {
            Schema schema = new Schema((type) => (IGraphType)serviceProvider.GetService(type))
            {
                Query = new BoxQuery(FolderService, ItemService)
            };
            ExecutionResult result = await new DocumentExecuter().ExecuteAsync(config =>
            {
                config.Schema = schema;
                config.Query = query;
            }).ConfigureAwait(false);

            if(result.Errors != null)
            {
                foreach(ExecutionError error in result.Errors)
                {
                    Logger.LogError(error.ToString());
                }
            }

            return new DocumentWriter(indent: true).Write(result);
        }
    }
}