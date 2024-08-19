using Infrastructure.Data.Entities.Base;

namespace Infrastructure.Data.Entities
{
    public class Category : Entity<int>
    {
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public IList<Product> Products { get; set; }

    }

}
