using Business.Models.Response;
using Business.Services.Base;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Infrastructure.Data.Entities;
using Infrastructure.Data.UnitOfWork;

namespace Business.Services
{
    public class RoleService : BaseService<Role, Guid, InfoRolDto>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IMapperHelper mapperHelper) : base(unitOfWork, unitOfWork.RoleRepository, mapperHelper)
        {
        }
    }
}
