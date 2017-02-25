using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PeopleSearch
{
    /// <summary>
    /// S
    /// </summary>
    public class Program
    {
        /// <summary>
        /// S
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(configuration)
                .UseContentRoot(Path.Combine(Directory.GetCurrentDirectory()))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
