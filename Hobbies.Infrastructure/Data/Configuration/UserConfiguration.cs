using Hobbies.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hobbies.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasData(SeedUsers());
            
        }
        private List<User> SeedUsers()
        {
            var hasher = new PasswordHasher<User>();
            var users = new List<User>();

            var admin = new User()
            {
                Email = "admin@gmail.com",
                UserName = "Admin",
                Id = "a91b540c-0c5e-484e-8eed-ba58172d1a14",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM"
            };

            admin.PasswordHash = hasher.HashPassword(admin, "admin123");
            users.Add(admin);

            var user = new User()
            {
                Email = "user@gmail.com",
                UserName = "Pesho",
                Id = "488d4eec-f740-4d33-8698-235bbb7ae9ba",
                NormalizedUserName = "PESHO",
                NormalizedEmail = "USER@GMAIL.COM"
            };
            user.PasswordHash = hasher.HashPassword(user, "user123");
            users.Add(user);

            return users;
        }
    }
}
