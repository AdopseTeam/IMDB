using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
