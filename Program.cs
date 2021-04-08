using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
// using MvcMovie.Models;
// using MvcActor.Models;
// using MvcSeries.Models;
using ElectronNET.API;

namespace IMDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static string HostPort => Environment.GetEnvironmentVariable("PORT")??"5000";
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseElectron(args);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
