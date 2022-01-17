using System;
using FM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FM.Infrastructure.Data.Seeds
{
    public class SeedAccountDefault
    {
        public static void SeedAccount(ModelBuilder builder)
        {
            Guid adminRoleId = Guid.Parse("18A45B0B-140B-48CE-9611-146F0D5912E1");
            Guid adminAccountId = Guid.Parse("60B1B5DF-96A3-42CF-8E39-E35F0955D9B1");
            string adminEmailDefault = "admin_default@gmail.com";

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN".ToUpper()
                });

            var hasherAdmin = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminAccountId,
                    Email = adminEmailDefault,
                    NormalizedEmail = adminEmailDefault.ToUpper(),
                    UserName = adminEmailDefault,
                    NormalizedUserName = adminEmailDefault.ToUpper(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Verified = DateTime.UtcNow,
                    PasswordHash = hasherAdmin.HashPassword(null, "Password@123")
                }
            );

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = adminRoleId,
                    UserId = adminAccountId,
                }
            );

            Guid userRoleId = Guid.Parse("D95B7821-FE71-4B83-BB40-7E27B547EF49");
            Guid userAccountId = Guid.Parse("8FD1E13C-78EC-466C-BBD2-423FA8F5F2EC");
            string userEmailDefault = "user_default@gmail.com";

            builder.Entity<ApplicationRole>()
                .HasData(new ApplicationRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER".ToUpper()
                });

            var hasherUser = new PasswordHasher<ApplicationUser>();

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = userAccountId,
                    Email = userEmailDefault,
                    NormalizedEmail = userEmailDefault.ToUpper(),
                    UserName = userEmailDefault,
                    NormalizedUserName = userEmailDefault.ToUpper(),
                    FirstName = "User",
                    LastName = "User",
                    Verified = DateTime.UtcNow,
                    PasswordHash = hasherUser.HashPassword(null, "Password@123")
                }
            );

            builder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = userRoleId,
                    UserId = userAccountId,
                }
            );
        }
    }
}