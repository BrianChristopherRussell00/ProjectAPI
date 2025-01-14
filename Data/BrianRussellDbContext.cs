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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Walk>().HasNoKey();  // Mark the entity as keyless
                                                     //Seed data for difficulties 
                                                     // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
        {
            new Difficulty()
            {
                Id = Guid.Parse("76bdad60-43f7-45d9-a191-80cc4599fdd2"),
                Name = "Easy "
            },
            new Difficulty()
            {
                Id = Guid.Parse("66ef759f-4193-43ca-9f73-50da9d3a7629"),
                Name = "Medium "
            },
            new Difficulty()
            {
                Id = Guid.Parse("aa022ac3-f5ca-4559-b30e-b735d50d7eff"),
                Name = "Hard "
            },
        };
            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for Region  

            var regions = new List<Region>

    {
        new Region
        {
       Id = Guid.Parse("caca6862-3fee-4746-b543-9613ab24c471"),
       Name= "Scranton",
       Code= "SCRT",
       RegionImageUrl=""

    },
         new Region
        {
       Id = Guid.Parse("37822212-77c4-4c26-8726-1db46bbd7b06"),
       Name= "Muncy",
       Code= "MUN",
       RegionImageUrl=""

    },
          new Region
        {
       Id = Guid.Parse("08bec0f6-b480-4a6e-874c-524507d70bcb"),
       Name= "York",
       Code= "YK",
       RegionImageUrl=""

    },
           new Region
        {
       Id = Guid.Parse("2974dcab-9fc7-4b1d-aecf-93cc27e3c93b"),
       Name= "Williamsport",
       Code= "WPT",
       RegionImageUrl=""

    }
    };
            modelBuilder.Entity<Region>().HasData(regions);
        }


    
        }


    }



