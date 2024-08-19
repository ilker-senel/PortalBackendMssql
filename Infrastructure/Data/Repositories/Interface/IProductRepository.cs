using Infrastructure.Data.Entities;
using Infrastructure.Data.Repositories.Base.Interface;

namespace Infrastructure.Data.Repositories.Interface
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
