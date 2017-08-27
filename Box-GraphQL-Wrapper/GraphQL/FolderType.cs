using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;
using Box_GraphQL_Wrapper.Interfaces;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    internal class FolderType : ObjectGraphType<BoxFolder>
    {
        public FolderType(IItemService itemService)
        {
            Field(x => x.Id).Description("The folder's ID");
            Field(x => x.Name).Description("The folder's name");
            Field<ListGraphType<ItemType>>("itemCollection", resolve: context => itemService.GetItems(context.Source.Id));
        }
    }
}
