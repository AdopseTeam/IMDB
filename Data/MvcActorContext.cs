using Microsoft.EntityFrameworkCore;
using MvcActor.Models;

namespace MvcActor.Data
{
    public class MvcActorContext : DbContext
    {
        public MvcActorContext (DbContextOptions<MvcActorContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Actor> Actor { get; set; }

    }
}