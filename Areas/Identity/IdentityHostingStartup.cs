using System;
using IMDB.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IMDB.Areas.Identity.IdentityHostingStartup))]
namespace IMDB.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        private string GetHerokuConnectionString()
        {
            string connectionUrl = "postgres://ldetimjpwraxvx:8dd3d702392b159b4e1503c1b86ed15ea1834349f8c0826fa8262cb214923f50@ec2-54-155-87-214.eu-west-1.compute.amazonaws.com:5432/d2jv7d6pmkrc6j";
            var databaseUri = new Uri(connectionUrl);
            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthUserDBContext>(options =>
                    options.UseNpgsql(GetHerokuConnectionString()));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AuthUserDBContext>();
            });
        }
    }
}