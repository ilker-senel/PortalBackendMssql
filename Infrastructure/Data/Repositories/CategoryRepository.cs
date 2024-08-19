using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Data.Repositories.Interface;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<IList<Category>> GetAllNonDeletedCategories()
        {
            return await base.GetAllAsync(c => c.IsDeleted == false);
        }

        public async Task<IList<Category>> GetAllDeletedCategories()
        {
            return await base.GetAllAsync(c => c.IsDeleted == true);
        }
    }
}
