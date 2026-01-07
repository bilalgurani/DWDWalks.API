using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DWDWalks.API.Data
{
    public class DWDWalksAuthDBContext : IdentityDbContext
    {
        public DWDWalksAuthDBContext(DbContextOptions<DWDWalksAuthDBContext> dbContextOptions) : base(dbContextOptions)
        {
           
        }
        
        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "18d8893e-c3f6-4316-bbf2-a51f4ae446ee";
            var writerRoleId = "2c927a08-02f7-4e13-862d-7cd214910e6a";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
