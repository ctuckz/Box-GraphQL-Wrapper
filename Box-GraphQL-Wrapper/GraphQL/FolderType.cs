﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;
using BoxGraphQLWrapper.Interfaces;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    internal class FolderType : ObjectGraphType<BoxFolder>
    {
        public FolderType(IItemService itemService)
        {
            Field(x => x.Id).Description("The folder's ID");
            Field(x => x.Name).Description("The folder's name");
            // Lazy load items - reduces response size
            Field<ListGraphType<ItemType>>("items", resolve: context => itemService.GetItems(context.Source.Id));
        }
    }
}
