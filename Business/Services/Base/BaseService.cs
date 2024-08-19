using Business.Services.Base.Interface;
using Business.Utilities.Mapping.Interface;
using Core.Constants;
using Core.Results;
using Infrastructure.Data.Repositories.Base.Interface;
using Infrastructure.Data.UnitOfWork;

namespace Business.Services.Base
{
    public abstract class BaseService<TEntity, TId, TResponseDto> : IBaseService<TEntity, TId, TResponseDto>
   where TEntity : class
   where TResponseDto : class
    {
        protected readonly IMapperHelper _mapperHelper;
        private readonly IRepository<TEntity, TId> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, IRepository<TEntity, TId> repository, IMapperHelper mapperHelper)
        {
            _mapperHelper = mapperHelper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<Result> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new Result(Messages.SuccessfullyCreatedEntity, ResultStatus.Ok);
        }

        public virtual async Task<Result> AddFromDtoAsync(object entityDto)
        {
            TEntity entity = _mapperHelper.Map<TEntity>(entityDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new Result(Messages.SuccessfullyCreatedEntity, ResultStatus.Ok);
        }

        public async Task<DataResult<IList<TResponseDto>>> GetAllAsync()
        {
            IList<TEntity> entities = await _repository.GetAllAsync();
            IList<TResponseDto> mappedEntities = _mapperHelper.Map<IList<TResponseDto>>(entities);
            return new DataResult<IList<TResponseDto>>(mappedEntities, "", ResultStatus.Ok);
        }

        public async Task<DataResult<TResponseDto>> GetByIdAsync(TId id)
        {
            TEntity entity = await _repository.GetByIdAsync(id);
            TResponseDto responseDto = _mapperHelper.Map<TResponseDto>(entity);
            return new DataResult<TResponseDto>(responseDto);
        }

        public async Task<Result> UpdateAsync(TId id, object entityDTO)
        {
            TEntity entity = await _repository.GetByIdAsync(id);

            _mapperHelper.Map(entityDTO, entity);
            _repository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new Result(Messages.SuccessfullyUpdatedEntity, ResultStatus.Ok);
        }

       

        public async Task<Result> HardDeleteByIdAsync(TId id)
        {
            await _repository.HardDeleteByIdAsync(id);
            await _unitOfWork.CommitAsync();
            return new Result(Messages.SuccessfullyDeletedEntity, ResultStatus.Ok);
        }


    }
}
