using Microsoft.EntityFrameworkCore;
using MvcActor.Models;

namespace MvcActor.Data
{
    public class MvcActorContext : DbContext
    {
        public MvcActorContext (DbContextOptions<MvcActorContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actor { get; set; }

        public DbSet<MvcActor.Models.Actor> Series { get; set; }
    }
}