﻿using System;
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
            ClientId = configSection.GetValue<string>("clientId") ?? throw new InvalidOperationException("ClientId was not configured.");
            ClientSecret = configSection.GetValue<string>("clientSecret") ?? throw new InvalidOperationException("ClientSecret was not configured.");
            DeveloperToken = configSection.GetValue<string>("developerToken") ?? throw new InvalidOperationException("DeveloperToken was not configured.");
        }

        public string ClientId { get; }
        public string ClientSecret { get; }
        public string DeveloperToken { get; }
    }
}
