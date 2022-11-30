using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hobbies.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(SeedUserRoles());
        }

        private List<IdentityUserRole<string>> SeedUserRoles()
        {
            var roles = new List<IdentityUserRole<string>>();

            roles.Add(new IdentityUserRole<string>
            {
                RoleId = "457552bb-4e96-43e5-a4d6-b76290144ac0",
                UserId = "a91b540c-0c5e-484e-8eed-ba58172d1a14"
            });

            return roles;
        }
    }
}
