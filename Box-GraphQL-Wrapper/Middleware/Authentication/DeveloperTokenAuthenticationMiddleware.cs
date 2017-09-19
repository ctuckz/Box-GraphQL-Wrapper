using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using BoxGraphQLWrapper.Interfaces;
using BoxGraphQLWrapper.Backend;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper.Middleware.Authentication
{
    public class DeveloperTokenAuthenticationMiddleware
    {
        private const string DeveloperSchemeName = "Developer";

        public DeveloperTokenAuthenticationMiddleware(RequestDelegate next, ILogger<DeveloperTokenAuthenticationMiddleware> logger)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private RequestDelegate Next { get; }
        private ILogger<DeveloperTokenAuthenticationMiddleware> Logger { get; }

        public async Task Invoke(HttpContext context)
        {
            string rawHeader = context.Request.Headers["Authorization"];
            if(rawHeader != null && 
                AuthenticationHeaderValue.TryParse(rawHeader, out AuthenticationHeaderValue authHeader) && 
                string.Equals(authHeader.Scheme, DeveloperSchemeName, StringComparison.CurrentCultureIgnoreCase))
            {
                AddClaimToPrincipal(context.User, authHeader.Parameter);
            }
            else
            {
                Logger.LogInformation("No Developer authentication header on request. Raw header: {rawHeader}", rawHeader);
            }

            await Next(context);
        }

        private static void AddClaimToPrincipal(ClaimsPrincipal currentPrincipal, string devToken)
        {
            Claim claim = new Claim("Developer", devToken);
            currentPrincipal.AddIdentity(new ClaimsIdentity(new[] { claim }, AuthenticationTypes.DeveloperAuthenticationType));
        }
    }
}
