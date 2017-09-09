using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Box_GraphQL_Wrapper.Interfaces
{
    public interface IDeveloperTokenProvider
    {
        bool TryGetDeveloperToken(out string token);
    }
}
