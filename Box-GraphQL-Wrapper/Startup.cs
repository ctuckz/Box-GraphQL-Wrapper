using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxGraphQLWrapper.Interfaces;
using BoxGraphQLWrapper.Authorization;
using BoxGraphQLWrapper.Backend;
using BoxGraphQLWrapper.Configuration;
using BoxGraphQLWrapper.Formatters;
using BoxGraphQLWrapper.GraphQL;
using BoxGraphQLWrapper.Middleware.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BoxGraphQLWrapper
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("authKeys.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureBoxServices(services);
            ConfigureGraphQLServices(services);

            // Add framework services.
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new GraphQLFormatter());
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            }
            else
            {
                // TODO add faster logging for non-dev environments
            }
            loggerFactory.AddDebug();

            app.UseMiddleware<DeveloperTokenAuthenticationMiddleware>();
            app.UseMvc();
        }

        private void ConfigureBoxServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationConfiguration, AuthenticationConfiguration>(sp => new AuthenticationConfiguration(Configuration));
            services.AddSingleton<IClientService, ClientService>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IDeveloperTokenProvider, DeveloperTokenProvider>();

            services.AddTransient<IFolderService, FolderService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IUserService, UserService>();
        }

        private static void ConfigureGraphQLServices(IServiceCollection services)
        {
            // Add all GraphQL *Type objects here. This allows us to DI services into them.
            services.AddTransient<FolderType>();
            services.AddTransient<ItemType>();
            services.AddTransient<UserType>();
        }
    }
}
