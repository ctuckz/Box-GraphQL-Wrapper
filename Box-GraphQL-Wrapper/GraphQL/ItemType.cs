using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    internal class ItemType : ObjectGraphType<BoxItem>
    {
        public ItemType()
        {
            Field(x => x.Id).Description("The item's ID");
            Field(x => x.Name).Description("The item's name");
            Field<IntGraphType>("size", description: "The item's size", resolve: context => context.Source.Size);
            Field<StringGraphType>("description", description: "The item's description", resolve: context => context.Source.Description);
            Field<ListGraphType<StringGraphType>>("tags", description: "The item's tags", resolve: context => context.Source.Tags);
            Field<DateGraphType>("createdAt", description: "The date which the item was created", resolve: context => context.Source.CreatedAt);
            Field<DateGraphType>("modifiedAt", description: "The date which the item was modified", resolve: context => context.Source.ModifiedAt);
            Field<FolderType>("parent", description: "The parent folder of this item", resolve: context => context.Source.Parent);
            Field<UserType>("ownedBy", description: "The user who owns this item", resolve: context => context.Source.OwnedBy);
            Field<UserType>("createdBy", description: "The user who created this item", resolve: context => context.Source.CreatedBy);
            Field<UserType>("modifiedBy", description: "The user who modified this item", resolve: context => context.Source.ModifiedBy);
        }
    }
}
