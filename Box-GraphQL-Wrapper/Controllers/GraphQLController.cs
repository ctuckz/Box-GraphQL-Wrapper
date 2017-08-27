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

namespace BoxGraphQLWrapper.Controllers
{
    [Produces("application/json")]
    [Route("api/GraphQL")]
    public class GraphQLController : Controller
    {
        private static HttpClient client = new HttpClient();

        public GraphQLController(IFolderService folderService)
        {
            FolderService = folderService ?? throw new ArgumentNullException(nameof(folderService), $"{nameof(IFolderService)} not configured.");
        }

        private IFolderService FolderService { get; }

        [HttpPost]
        public async Task<string> GraphQL([FromBody] string query)
        {
            Schema schema = new Schema { Query = new BoxQuery(FolderService) };
            ExecutionResult result = await new DocumentExecuter().ExecuteAsync(config =>
            {
                config.Schema = schema;
                config.Query = query;
            }).ConfigureAwait(false);

            return new DocumentWriter(indent: true).Write(result);
        }
    }
}