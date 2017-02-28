using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Data;
using PeopleSearch.Settings;

namespace PeopleSearch
{
    /// <summary>
    /// a
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        /// <summary>
        /// a
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Add application settings
            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            // Add database context object
            var connection = Configuration["Data:SqliteConnectionString"];
            services.AddDbContext<PeopleSearch.Data.PersonSearchingContext>( options => options.UseSqlite(connection));

            services.AddMemoryCache();
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            
            // added chain of responsibility to DI framework
            services.AddTransient<DataHandler, TrieHandler> (
                (sp) => 
                {
                    //Bugs with TrieHandler :(
                    var result = new TrieHandler(sp.GetService<IMemoryCache>(), sp.GetService<PersonSearchingContext>());
                    result.SetSuccessor(new DatabaseHandler(sp.GetService<PersonSearchingContext>()));
                    return result; //new DatabaseHandler(sp.GetService<PersonSearchingContext>());
                });

            services.AddMvc(options => options.MaxModelValidationErrors = 50);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Content")),
                RequestPath = new PathString("/content")
            });
            
            app.UseMvc();
        }
    }
}
