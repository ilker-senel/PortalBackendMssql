using Business.Models.Response;
using Business.Services.Base.Interface;
using Infrastructure.Data.Entities;

namespace Business.Services.Interface
{
    public interface ICategoryService : IBaseService<Category, int, InfoCategoryDto>
    {
        Task<IList<InfoCategoryDto>> GetAllNonDeletedCategories();
        Task<IList<InfoCategoryDto>> GetAllDeletedCategories();
    }
}
