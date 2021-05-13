using IMDB.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MvcActor.Models;
using MvcMovie.Models;
using MvcSeries.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using Microsoft.AspNetCore.Identity;

namespace IMDB.Data
{
    public class AuthUserDBContext : IdentityDbContext<IdentityUser>
    {

        public AuthUserDBContext(DbContextOptions<AuthUserDBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
