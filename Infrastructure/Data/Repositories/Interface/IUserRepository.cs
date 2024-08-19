using Infrastructure.Data.Entities;
using Infrastructure.Data.Repositories.Base.Interface;

namespace Infrastructure.Data.Repositories.Interface
{
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
