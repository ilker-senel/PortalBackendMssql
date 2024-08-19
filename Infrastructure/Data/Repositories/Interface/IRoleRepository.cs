using Infrastructure.Data.Entities;
using Infrastructure.Data.Entities.Base;
using Infrastructure.Data.Repositories.Base.Interface;

namespace Infrastructure.Data.Repositories.Interface
{
    public interface IRoleRepository : IRepository<Role, Guid>
    {
    }
}
