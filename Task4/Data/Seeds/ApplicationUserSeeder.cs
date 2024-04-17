using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task4.Entities;

namespace Task4.Data.Seeds
{
    internal static class ApplicationUserSeeder
    {
        internal static void SeedApplicationUsers(this ModelBuilder builder)
        {
            var users = new List<ApplicationUser>() {
                new ApplicationUser()
                {
                    Id = "1e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "user1",
                    NormalizedUserName = "USER1".ToUpper(),
                    Name = "user1",
                    Email = "ko066@gmail.com",
                    Status = Enums.StatusEnum.Unblock,
                    Registered = new DateTime(2010,10,10),
                    LastLogined = new DateTime(2010,10,10)
                },
                new ApplicationUser()
                {
                    Id = "2e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "user2",
                    NormalizedUserName = "USER2".ToUpper(),
                    Name = "user2",
                    Email = "some@gmail.com",
                    Status = Enums.StatusEnum.Unblock,
                    Registered = new DateTime(2010,10,10),
                    LastLogined = new DateTime(2010,10,10)
                },
                new ApplicationUser()
                {
                    Id = "3e445865-a24d-4543-a6c6-9443d048cdb9",
                    UserName = "user3",
                    NormalizedUserName = "USER3".ToUpper(),
                    Name = "user3",
                    Email = "new66@gmail.com",
                    Status = Enums.StatusEnum.Unblock,
                    Registered = new DateTime(2010,10,10),
                    LastLogined = new DateTime(2010,10,10)
                },
            };

            var hasher = new PasswordHasher<ApplicationUser>();

            users[0].PasswordHash = hasher.HashPassword(users[0], "user1");
            users[1].PasswordHash = hasher.HashPassword(users[1], "user2");
            users[2].PasswordHash = hasher.HashPassword(users[2], "user3");

            builder.Entity<ApplicationUser>().HasData(users);
        }
    }
}
