using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGraphQLWrapper.Interfaces
{
    public interface IAuthenticationConfiguration
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}
