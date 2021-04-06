using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Models;
using MvcActor.Models;
using MvcSeries.Models;
using ElectronNET.API;

namespace IMDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                  SeedMovies.Initialize(services);
                  SeedSeries.Initialize(services);
                  SeedActors.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static string HostPort => Environment.GetEnvironmentVariable("PORT")??"5000";
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseElectron(args);
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.ListenAnyIP(Int32.Parse(HostPort));
                    });
                });
    }
}
