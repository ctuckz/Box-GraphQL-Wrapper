using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box_GraphQL_Wrapper.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BoxGraphQLWrapper.Configuration
{
    public class AuthenticationConfiguration : IAuthenticationConfiguration
    {
        public AuthenticationConfiguration(IConfigurationRoot config)
        {
            IConfigurationSection configSection = config.GetSection("auth") ?? throw new InvalidOperationException("Could not find auth configuration.");
            ClientId = configSection.GetValue<string>("clientId");
            ClientSecret = configSection.GetValue<string>("clientSecret");
            DeveloperToken = configSection.GetValue<string>("developerToken");
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
        public string DeveloperToken { get; }
    }
}
