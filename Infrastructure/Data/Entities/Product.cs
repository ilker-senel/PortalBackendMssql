using Infrastructure.Data.Entities.Base;

namespace Infrastructure.Data.Entities
{
    public class Product : Entity<int>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
