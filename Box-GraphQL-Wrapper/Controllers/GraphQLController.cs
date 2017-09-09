using System;
using System.Threading.Tasks;
using BoxGraphQLWrapper.Interfaces;
using BoxGraphQLWrapper.Middleware.FieldMiddleware;
using BoxGraphQLWrapper.GraphQL;
using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper.Controllers
{
    [Produces("application/json")]   
    public class GraphQLController : Controller
    {
        public GraphQLController(IFolderService folderService, IItemService itemService, IUserService userService, ILogger<GraphQLController> logger)
        {
            FolderService = folderService ?? throw new ArgumentNullException(nameof(folderService), $"{nameof(IFolderService)} not configured.");
            ItemService = itemService ?? throw new ArgumentNullException(nameof(itemService), $"{nameof(IItemService)} not configured.");
            UserService = userService ?? throw new ArgumentNullException(nameof(userService), $"{nameof(IUserService)} not configured.");
            Logger = logger ?? throw new ArgumentNullException(nameof(logger), $"{nameof(ILogger<GraphQLController>)} not configured.");
        }

        private IFolderService FolderService { get; }
        private IItemService ItemService { get; }
        private IUserService UserService { get; }
        private ILogger<GraphQLController> Logger { get; }

        [HttpPost]
        [Route("api/GraphQL")]
        public async Task<string> GraphQL([FromBody] string query, [FromServices] IServiceProvider serviceProvider)
        {
            Schema schema = new Schema((type) => (IGraphType)serviceProvider.GetService(type))
            {
                Query = new BoxQuery(FolderService, ItemService, UserService)
            };

            ExecutionResult result = await new DocumentExecuter().ExecuteAsync(config =>
            {
                config.Schema = schema;
                config.Query = query;
                config.FieldMiddleware.Use<AuthenticationErrorMiddleware>();
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