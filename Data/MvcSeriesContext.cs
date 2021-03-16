using Microsoft.EntityFrameworkCore;
using MvcSeries.Models;

namespace MvcSeries.Data
{
    public class MvcSeriesContext : DbContext
    {
        public MvcSeriesContext (DbContextOptions<MvcSeriesContext> options)
            : base(options)
        {
        }

        public DbSet<Series> Series { get; set; }
    }
}