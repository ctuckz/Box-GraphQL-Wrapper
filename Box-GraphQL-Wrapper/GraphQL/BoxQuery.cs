using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box_GraphQL_Wrapper.Interfaces;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    public class BoxQuery : ObjectGraphType
    {
        public BoxQuery(IFolderService folderService)
        {
            Field<FolderType>("folder",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context =>
                {
                    string id = context.GetArgument<string>("id");
                    return folderService.GetFolderById(id);
                });
        }
    }
}
