using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IDeveloperTokenProvider
    {
        bool TryGetDeveloperToken(out string token);
    }
}
