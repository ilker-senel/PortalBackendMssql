
using Core.Utilities;
using Infrastructure.Data.Entities.Base.Interface;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;

        private UserRepository userRepository;
        private RoleRepository roleRepository;

        private ProductRepository productRepository;
        private CategoryRepository categoryRepository;

        public UnitOfWork(AppDbContext dbContext, ProductRepository productRepository, CategoryRepository categoryRepository, UserRepository userRepository, RoleRepository roleRepository)
        {
            this.dbContext = dbContext;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        public IProductRepository ProductRepository => productRepository ??= new ProductRepository(dbContext);
        public ICategoryRepository CategoryRepository => categoryRepository ??= new CategoryRepository(dbContext);

        public IUserRepository UserRepository => userRepository ??= new UserRepository(dbContext);
        public IRoleRepository RoleRepository => roleRepository ??= new RoleRepository(dbContext);

        public async Task<int> CommitAsync()
        {
            var updatedEntities = dbContext.ChangeTracker.Entries<IEntity>()
            .Where(e => e.State == EntityState.Modified)
            .Select(e => e.Entity);

            foreach (var updatedEntity in updatedEntities)
            {
                updatedEntity.UpdatedAt = DateTime.UtcNow.ToTimeZone();
            }

            var result = await dbContext.SaveChangesAsync();

            return result;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
