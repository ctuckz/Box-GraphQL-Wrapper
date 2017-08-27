using System;
using System.IO;
using System.Threading;
using System.Net.Http;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace BoxGraphQLWrapper.Formatters
{
    public class GraphQLFormatter : TextInputFormatter
    {
        public GraphQLFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/graphql"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body))
            {
                string str = reader.ReadToEnd();
                return await InputFormatterResult.SuccessAsync(str);
            }            
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(string) || type == typeof(ExecutionResult) || type is IObjectGraphType;
        }
    }
}
