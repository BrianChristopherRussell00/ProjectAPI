using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using ProjectAPI.Models.Domain;

namespace ProjectAPI.Data
{
    public class BrianRussellDbContext : DbContext
    {
        public BrianRussellDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Walk>()
                .HasNoKey();  // Mark the entity as keyless
        }
    }
    public class NZWalksDbContextFactory : IDesignTimeDbContextFactory<BrianRussellDbContext>
    {
        public BrianRussellDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BrianRussellDbContext>();
            optionsBuilder.UseSqlServer("BRConnectionString");

            return new BrianRussellDbContext(optionsBuilder.Options);
        }
    }


}


