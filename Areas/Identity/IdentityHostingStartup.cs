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
            string connectionUrl = "postgres://xsoczdteqywkuc:3ba7a0591aab685bf97bed07c92bc3edaef0ac6c9245b1ae31fb68cb68a406a7@ec2-54-155-92-75.eu-west-1.compute.amazonaws.com:5432/d4lfkc2vp5brs5";

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

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AuthUserDBContext>();

            });
        }
    }
}