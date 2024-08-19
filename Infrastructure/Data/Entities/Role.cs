using Infrastructure.Data.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Entities
{
    public class Role : Entity<Guid>
    {
        public string RoleName { get; set; }
        public IList<User> Users { get; set; }
    }
}
