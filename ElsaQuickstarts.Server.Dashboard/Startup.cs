using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Persistence.EntityFramework.Sqlite;
using Elsa.Scripting.Liquid.Services;
using ElsaQuickstarts.Server.Dashboard.Bookmarks;
using ElsaQuickstarts.Server.Dashboard.Javascript;
using ElsaQuickstarts.Server.Dashboard.Liquid;
using ElsaQuickstarts.Server.Dashboard.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElsaQuickstarts.Server.Dashboard
{

    //https://elsa-workflows.github.io/elsa-core/docs/next/quickstarts/quickstarts-aspnetcore-server-dashboard-and-api-endpoints#next-steps
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IWebHostEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var elsaSection = Configuration.GetSection("Elsa");

            // Elsa services.
            services
                .AddElsa(elsa => elsa
                    .UseEntityFrameworkPersistence(ef => ef.UseSqlite())
                    .AddConsoleActivities()
                    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
                    .AddEmailActivities(elsaSection.GetSection("Smtp").Bind)
                    .AddQuartzTemporalActivities()  
                    .AddActivitiesFrom<Startup>()
                    .AddWorkflowsFrom<Startup>()                    

                );

            services.AddBookmarkProvidersFrom<Startup>();
            services.AddScoped<IFileReceivedInvoker, FileReceivedInvoker>();
            // Elsa API endpoints.
            services.AddElsaApiEndpoints();

            //Elsa automatically generate a type definition based on a given class.
            services.AddJavaScriptTypeDefinitionProvider<MyTypeDefinitionProvider>();
            services.AddNotificationHandlersFrom<MyTypeLiquidHandler>();

            // Allow arbitrary client browser apps to access the API.
            // In a production environment, make sure to allow only origins you trust.
            services.AddCors(cors => cors.AddDefaultPolicy(policy => policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .WithExposedHeaders("Content-Disposition"))
            );

            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
            services.AddHostedService<FileMonitorService>();

            // For Dashboard.
            services.AddRazorPages();

            services.AddSingleton(Console.Out);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseCors()
                .UseStaticFiles() // For Dashboard.
                .UseHttpActivities()
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    // Elsa API Endpoints are implemented as regular ASP.NET Core API controllers.
                    endpoints.MapControllers();

                    // For Dashboard.
                    endpoints.MapFallbackToPage("/_Host");
                });
        }
    }
}
