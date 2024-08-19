using Infrastructure.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Entities
{
    public class User : Entity<Guid>
    {
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public Gender Gender { get; set; } = default!;
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; } = default!;
        public string RefreshToken { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

    }
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
}
