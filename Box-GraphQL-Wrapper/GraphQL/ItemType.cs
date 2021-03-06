﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using BoxGraphQLWrapper.Interfaces;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    internal class ItemType : ObjectGraphType<BoxItem>
    {
        public ItemType(IUserService userService, IItemContentService itemContentService)
        {
            Field(x => x.Id).Description("The item's ID");
            Field(x => x.Name).Description("The item's name");
            Field<IntGraphType>("size", description: "The item's size", resolve: context => context.Source.Size);
            Field<StringGraphType>("description", description: "The item's description", resolve: context => context.Source.Description);
            Field<ListGraphType<StringGraphType>>("tags", description: "The item's tags", resolve: context => context.Source.Tags);
            Field<DateGraphType>("createdAt", description: "The date which the item was created", resolve: context => context.Source.CreatedAt);
            Field<DateGraphType>("modifiedAt", description: "The date which the item was modified", resolve: context => context.Source.ModifiedAt);
            Field<FolderType>("parent", description: "The parent folder of this item", resolve: context => context.Source.Parent);
            Field<UserType>("ownedBy", description: "The user who owns this item", resolve: context => userService.GetUserById(context.Source.OwnedBy?.Id));
            Field<UserType>("createdBy", description: "The user who created this item", resolve: context => userService.GetUserById(context.Source.CreatedBy?.Id));
            Field<UserType>("modifiedBy", description: "The user who modified this item", resolve: context => userService.GetUserById(context.Source.ModifiedBy?.Id));
            Field<StringGraphType>("contentUri", 
                description: "The location where the file's content can be downloaded from", 
                resolve: context => itemContentService.GetItemDownloadUri(context.Source.Id));
        }
    }
}
