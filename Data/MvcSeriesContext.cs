using Microsoft.EntityFrameworkCore;
using MvcSeries.Models;
using Newtonsoft.Json.Linq;
using System;

namespace MvcSeries.Data
{
    public class MvcSeriesContext : DbContext
    {
        public MvcSeriesContext (DbContextOptions<MvcSeriesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Series> Series { get; set; }
    }
}