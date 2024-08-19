using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Data.Repositories.Interface;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

    }
}
