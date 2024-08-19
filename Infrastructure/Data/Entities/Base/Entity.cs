using Core.Utilities;
using Infrastructure.Data.Entities.Base.Interface;

namespace Infrastructure.Data.Entities.Base
{
    public class Entity<T> : IEntity
    {
        public T Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        protected Entity()
        {
            CreatedAt = DateTime.UtcNow.ToTimeZone();
            IsDeleted = false;
        }
    }


}
