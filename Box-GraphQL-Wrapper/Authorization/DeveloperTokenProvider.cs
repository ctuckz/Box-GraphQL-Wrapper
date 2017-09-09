using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Box_GraphQL_Wrapper.Interfaces;
using Microsoft.AspNetCore.Http;

namespace BoxGraphQLWrapper.Authorization
{
    public class DeveloperTokenProvider : IDeveloperTokenProvider
    {
        public DeveloperTokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private IHttpContextAccessor HttpContextAccessor { get; }

        public bool TryGetDeveloperToken(out string token)
        {
            token = null;

            ClaimsIdentity developerIdentity = HttpContextAccessor.HttpContext.User.Identities
                .FirstOrDefault(i => string.Equals(i.AuthenticationType, AuthenticationTypes.DeveloperAuthenticationType));
            if(developerIdentity == null)
            {
                return false;
            }

            Claim developerClaim = developerIdentity.FindFirst("Developer");
            if(developerClaim == null)
            {
                return false;
            }

            token = developerClaim.Value;
            return true;
        }
    }
}
