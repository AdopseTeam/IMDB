using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Data
{
    public class AuthUserDBContext : IdentityDbContext<ApplicationUser>
    {

        public AuthUserDBContext(DbContextOptions<AuthUserDBContext> options)
            : base(options)
        {
        }
        //public DbSet<MvcMovie.Models.Movies> Movies { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(m => m.MoviesList)
                .WithOne(u => u.User)
                .IsRequired();

            builder.Entity<MvcMovie.Models.Movies>()
                .HasMany(w => w.Watchlists)
                .WithOne(m => m.Movie)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .HasMany(w => w.Watchlists)
                .WithOne(u => u.User)
                .IsRequired();

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
