using Business.Models.Response;
using Business.Services.Base.Interface;
using Infrastructure.Data.Entities;

namespace Business.Services.Interface
{
    public interface IRoleService : IBaseService<Role, Guid, InfoRolDto>
    {
    }
}
