using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2.Models;
using GraphQL.Types;

namespace BoxGraphQLWrapper.GraphQL
{
    internal class FolderType : ObjectGraphType<BoxFolder>
    {
        public FolderType()
        {
            Field(x => x.Id).Description("The folder's ID");
            Field(x => x.Name).Description("The folder's name");
        }
    }
}
