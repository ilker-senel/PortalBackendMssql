using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories.Base;
using Infrastructure.Data.Repositories.Interface;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

    }

}
