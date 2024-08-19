using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Data.Repositories.Interface;

namespace Infrastructure.Data.Repositories
{
    public class RoleRepository : Repository<Role, Guid>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context) { }
    }
}
