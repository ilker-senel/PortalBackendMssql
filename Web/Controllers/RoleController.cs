using Business.Models.Request.Create;
using Business.Models.Request.Update;
using Business.Models.Response;
using Business.Services.Interface;
using Core.Results;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class RoleController : Base.BaseController
    {
        private readonly IRoleService service;

        public RoleController(IRoleService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Result>> CreateRole([FromBody] CreateRolDto dto)
        {
            dto.Id = new Guid();
            return await service.AddFromDtoAsync(dto);
        }

        [HttpPut]
        public async Task<ActionResult<Result>> UpdateRole(Guid id, [FromBody] UpdateRolDto dto)
        {
            return await service.UpdateAsync(id, dto);
        }

        [HttpGet]
        public async Task<ActionResult<DataResult<IList<InfoRolDto>>>> GetAllRoles()
        {
            return await service.GetAllAsync();
        }
    }
}
