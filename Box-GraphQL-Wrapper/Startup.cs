using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Box_GraphQL_Wrapper.Interfaces;
using BoxGraphQLWrapper.Backend;
using BoxGraphQLWrapper.Configuration;
using BoxGraphQLWrapper.Formatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("authKeys.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureBoxServices(services);

            // Add framework services.
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new GraphQLFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        private void ConfigureBoxServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationConfiguration, AuthenticationConfiguration>(sp => new AuthenticationConfiguration(Configuration));
            services.AddSingleton<IClientService, ClientService>();

            services.AddTransient<IFolderService, FolderService>();
        }
    }
}
