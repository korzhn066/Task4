using Microsoft.AspNetCore.Identity;
using System.Data;
using Task4.Enums;

namespace Task4.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public DateTime Registered { get; set; }
        public DateTime LastLogined { get; set; }

        public StatusEnum Status { get; set; }
    }
}
