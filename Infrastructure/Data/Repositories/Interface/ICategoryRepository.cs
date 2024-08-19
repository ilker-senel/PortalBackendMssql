using Infrastructure.Data.Entities;
using Infrastructure.Data.Repositories.Base.Interface;

namespace Infrastructure.Data.Repositories.Interface
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<IList<Category>> GetAllNonDeletedCategories();
        Task<IList<Category>> GetAllDeletedCategories();
    }
}
