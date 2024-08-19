using Business.Models.Request.Create;
using Business.Models.Request.Delete;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CategoryController : Base.BaseController
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<DataResult<IList<InfoCategoryDto>>>> GetAllCategories()
        {
            return await service.GetAllAsync();
        }
        [HttpGet]
        public async Task<ActionResult<DataResult<InfoCategoryDto>>> GetCategoryById(int categoryId)
        {
            return await service.GetByIdAsync(categoryId);
        }

        [HttpGet]
        public async Task<ActionResult<DataResult<IList<InfoCategoryDto>>>> GetAllDeletedCategories()
        {
            var categories = await service.GetAllDeletedCategories();
            return new DataResult<IList<InfoCategoryDto>>(categories, "", ResultStatus.Ok);

        }

        [HttpGet]
        public async Task<ActionResult<DataResult<IList<InfoCategoryDto>>>> GetAllNonDeletedCategories()
        {
            var categories = await service.GetAllNonDeletedCategories();
            return new DataResult<IList<InfoCategoryDto>>(categories, "", ResultStatus.Ok);

        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            return await service.AddFromDtoAsync(dto);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            return await service.UpdateAsync(id, dto);
        }

        [HttpDelete]
        public async Task<ActionResult<Result>> HardDeleteCategory(int categoryId)
        {
            return await service.HardDeleteByIdAsync(categoryId);
        }
        [HttpPut]
        public async Task<ActionResult<Result>> DeleteCategory(int id)
        {
            var dto = new DeleteDto();
            dto.isDeleted = true;
            return await service.UpdateAsync(id, dto);
        }
    }
}
