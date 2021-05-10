using IMDB.Models;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcComments.Models;
using Newtonsoft.Json.Linq;
using System;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<Watchlist> Watchlist { get; set; }
        public DbSet<Comments> Comments {get; set;}

    }
}