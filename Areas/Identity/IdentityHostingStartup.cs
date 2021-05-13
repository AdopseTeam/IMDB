using System;
using IMDB.Data;
using IMDB.Models;
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
            string connectionUrl = "postgres://obpbfczizjlyba:d38ff371a3ce6923976d077d6747d1a3956b799fca8485503088d279e42e4713@ec2-99-80-200-225.eu-west-1.compute.amazonaws.com:5432/d9ai4prm99v142";
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