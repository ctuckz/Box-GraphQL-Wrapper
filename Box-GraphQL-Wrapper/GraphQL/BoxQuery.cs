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
        public BoxQuery(IFolderService folderService, IItemService itemService)
        {
            Field<FolderType>("folder",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context =>
                {
                    string id = context.GetArgument<string>("id");
                    return folderService.GetFolderById(id);
                });
            Field<ListGraphType<ItemType>>("folderItems",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "folderId" }),
                resolve: context =>
                {
                    string folderId = context.GetArgument<string>("folderId");
                    return itemService.GetItems(folderId);
                });
        }
    }
}
