using Business.Models.Response;
using Business.Services.Base;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Core.Results;
using Infrastructure.Data.Entities;
using Infrastructure.Data.UnitOfWork;

namespace Business.Services
{
    public class CategoryService : BaseService<Category, int, InfoCategoryDto>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapperHelper mapperHelper) : base(unitOfWork, unitOfWork.CategoryRepository, mapperHelper)
        {
        }

        public async Task<IList<InfoCategoryDto>> GetAllDeletedCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllDeletedCategories();
            return _mapperHelper.Map<List<InfoCategoryDto>>(categories);

        }

        public async Task<IList<InfoCategoryDto>> GetAllNonDeletedCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllNonDeletedCategories();
            return _mapperHelper.Map<List<InfoCategoryDto>>(categories);
        }
    }
}
