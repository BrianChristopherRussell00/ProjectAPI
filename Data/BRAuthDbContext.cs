using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectAPI.Data
{
    public class BRAuthDbContext : IdentityDbContext
    {
        public BRAuthDbContext(DbContextOptions<BRAuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
           base.OnModelCreating(builder);
            var readerRoleId = "af378ef1-4f66-4ae4-8dce-6ccb2cb2e8e0";
            var writerRoleId = "28995407-f823-4dc2-8a8e-f594fc3a1692";

            var roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Id =readerRoleId,   
                ConcurrencyStamp=readerRoleId,  
                Name="Reader",  
                NormalizedName="Reader".ToUpper()
            },  
            new IdentityRole
            {
                 Id =writerRoleId,
                ConcurrencyStamp=writerRoleId,
                Name="Writer",
                NormalizedName="Writer".ToUpper()


            }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
