using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hobbies.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(SeedRoles());
        }

        private List<IdentityRole> SeedRoles()
        {
            var roles = new List<IdentityRole>();

            roles.Add(new IdentityRole
            {
                Id = "457552bb-4e96-43e5-a4d6-b76290144ac0",
                Name = "Admin"
            });

            return roles;
        }
    }
}
