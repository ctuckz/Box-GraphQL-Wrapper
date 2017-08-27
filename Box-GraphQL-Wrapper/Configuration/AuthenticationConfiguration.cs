using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxGraphQLWrapper.Configuration
{
    public class AuthenticationConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DeveloperToken { get; set; }
    }
}
