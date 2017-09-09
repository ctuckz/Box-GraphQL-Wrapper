using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box.V2.Exceptions;
using GraphQL;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace BoxGraphQLWrapper.FieldMiddleware
{
    public class AuthenticationErrorMiddleware
    {
        public async Task<object> Resolve(
        ResolveFieldContext context,
        FieldMiddlewareDelegate next)
        {
            try
            {
                return await next(context);
            }
            catch(BoxSessionInvalidatedException)
            {
                context.Errors.Add(new ExecutionError("Could not obtain a valid session with Box."));
                throw;
            }
        }
    }
}
