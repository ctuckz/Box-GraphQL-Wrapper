using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGraphQLWrapper.Backend.Caching
{
    public struct CacheKey<T>
    {
        public CacheKey(string accessToken, string id)
        {
            AccessToken = accessToken;
            Id = id;
        }

        public string AccessToken { get; }
        public string Id { get; }
    }
}
