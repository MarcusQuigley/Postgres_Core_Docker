using Microsoft.EntityFrameworkCore;
using Postgres_Core_Docker.Models;

namespace Postgres_Core_Docker.DbContexts
{
    public class ItemsDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public ItemsDbContext(DbContextOptions<ItemsDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}