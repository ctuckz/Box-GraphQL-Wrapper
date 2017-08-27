using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Models;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    public class UserType : ObjectGraphType<BoxUser>
    {
        public UserType()
        {
            Field(x => x.Id).Description("The user's id");
            Field(x => x.Name).Description("The user's name");
            Field(x => x.IsExemptFromLoginVerification).Description("Whether the user is exempt from login verification");
            Field(x => x.IsExemptFromDeviceLimits).Description("Whether the user is exempty from device limits");
            Field(x => x.AvatarUrl, nullable: true).Description("The user's avatar URL");
            Field(x => x.Address, nullable: true).Description("The user's address");
            Field(x => x.Phone, nullable: true).Description("The user's phone number");
            Field(x => x.JobTitle, nullable: true).Description("The user's job title");
            Field(x => x.Status, nullable: true).Description("The user's status");
            Field<BooleanGraphType>("isSyncEnabled", description: "Whether sync is enabled", resolve: context => context.Source.IsSyncEnabled);
            Field<BooleanGraphType>("canSeeManagedUsers", description: "Whether the user can see managed users", resolve: context => context.Source.CanSeeManagedUsers);
            Field<IntGraphType>("maxUploadSize", description: "The user's max upload size", resolve: context => context.Source.MaxUploadSize);
            Field<IntGraphType>("spaceUsed", description: "The total space used by the user", resolve: context => context.Source.SpaceUsed);
            Field<IntGraphType>("spaceAmount", description: "THe user's total available space", resolve: context => context.Source.SpaceAmount);
            Field(x => x.Language, nullable: true).Description("The user's language");
            Field(x => x.Role, nullable: true).Description("The user's role");
            Field<DateGraphType>("modifiedAt", description: "The date which the user was modified", resolve: context => context.Source.ModifiedAt);
            Field<DateGraphType>("createdAt", description: "The date which the user was created", resolve: context => context.Source.CreatedAt);
            Field(x => x.Login, nullable: true).Description("The email used by the user to login");

        }
    }
}
