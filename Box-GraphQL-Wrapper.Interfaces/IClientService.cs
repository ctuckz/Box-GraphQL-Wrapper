using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Box.V2;

namespace Box_GraphQL_Wrapper.Interfaces
{
    public interface IClientService
    {
        BoxClient GetClient();
    }
}
