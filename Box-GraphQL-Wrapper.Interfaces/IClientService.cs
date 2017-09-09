using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IClientService
    {
        BoxClient GetClient();
    }
}
