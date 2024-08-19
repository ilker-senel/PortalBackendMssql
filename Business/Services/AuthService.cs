using Business.Models.Request.Functional;
using Business.Models.Response;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Business.Utilities.Security.Auth.Interface;
using Business.Utilities.Validation.Interface;
using Core.Constants;
using Core.Results;
using Core.Utilities;
using Infrastructure.Data.Entities;
using Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Token = Core.Constants.Token;


namespace Business.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimHelper _claimHelper;
    private readonly IMapperHelper _mapperHelper;
    private readonly IValidationHelper _validationHelper;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    private readonly IHashingHelper _hashingHelper;

    public AuthService(IUnitOfWork unitOfWork,
        IClaimHelper claimHelper,
        IMapperHelper mapperHelper,
        IValidationHelper validationHelper,
        IJwtTokenHelper jwtTokenHelper,
        IHashingHelper hashingHelper)
    {
        _unitOfWork = unitOfWork;
        _claimHelper = claimHelper;
        _mapperHelper = mapperHelper;
        _validationHelper = validationHelper;
        _jwtTokenHelper = jwtTokenHelper;
        _hashingHelper = hashingHelper;
    }

    public async Task<DataResult<Utilities.Security.Auth.Token>> Register(RegisterDto registerDto)
    {
        var validationError = await _validationHelper.ValidateAsync(registerDto);

        if (!string.IsNullOrEmpty(validationError))
            return new DataResult<Utilities.Security.Auth.Token>(message: validationError,
                status: ResultStatus.Invalid);

        if (await _unitOfWork.UserRepository.GetAsync(u => u.UserName == registerDto.UserName) != null)
            return new DataResult<Utilities.Security.Auth.Token>(message: Messages.UserNameAlreadyTaken,
                status: ResultStatus.Invalid);

        if (await _unitOfWork.UserRepository.GetAsync(u => u.Email == registerDto.Email) != null)
            return new DataResult<Utilities.Security.Auth.Token>(message: Messages.EmailAlreadyTaken,
                status: ResultStatus.Invalid);

        var user = _mapperHelper.Map<User>(registerDto);
        user.RefreshToken = Guid.NewGuid().ToString();
        user.ExpiryTime = DateTime.UtcNow.Date.ToTimeZone().AddDays(Token.RefreshTokenValidUntilDays);

        _hashingHelper.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;

        user.PasswordSalt = passwordSalt;
        var role = await _unitOfWork.RoleRepository.GetAsync(r => r.RoleName == "USER");
        user.RoleId = role.Id;

        user.Id = new Guid();


        await _unitOfWork.UserRepository.AddAsync(user);

        await _unitOfWork.CommitAsync();
        user.Role = role;

        var token = _jwtTokenHelper.CreateAccessToken(user, user.RefreshToken);

        return new DataResult<Utilities.Security.Auth.Token>(data: token, status: ResultStatus.Ok);
    }

    public async Task<DataResult<Utilities.Security.Auth.Token>> Login(LoginDto loginDto)
    {
        var validationError = await _validationHelper.ValidateAsync(loginDto);

        if (!string.IsNullOrEmpty(validationError))
        {
            return new DataResult<Utilities.Security.Auth.Token>(message: validationError, status: ResultStatus.Invalid);
        }

        var user = await _unitOfWork.UserRepository.GetAsync(x => x.Email == loginDto.Email,
         include: query => query.Include(u => u.Role));

        if (user == null || !_hashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new DataResult<Utilities.Security.Auth.Token>(message: Messages.UserNameOrPasswordWrong, status: ResultStatus.Invalid);
        }

        user.RefreshToken = Guid.NewGuid().ToString();
        user.ExpiryTime = DateTime.UtcNow.Date.ToTimeZone().AddDays(Token.RefreshTokenValidUntilDays);

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.CommitAsync();
        

        var token = _jwtTokenHelper.CreateAccessToken(user, user.RefreshToken);

        return new DataResult<Utilities.Security.Auth.Token>(data: token, status: ResultStatus.Ok);
    }

    public async Task<DataResult<UserProfileDto>> GetUserProfileInfo()
    {
        var userId = _claimHelper.GetUserId();

        var user = await _unitOfWork.UserRepository.GetAsync(u => u.Id.ToString() == userId);

        var profileDto = _mapperHelper.Map<UserProfileDto>(user);

        return new DataResult<UserProfileDto>(data: profileDto, status: ResultStatus.Ok);
    }

    public async Task<DataResult<Utilities.Security.Auth.Token>> RefreshToken(string refreshToken)
    {
        var user = await _unitOfWork.UserRepository.GetAsync(u => u.RefreshToken == refreshToken);

        if (user == null)
        {
            return new DataResult<Utilities.Security.Auth.Token>(status: ResultStatus.Invalid);
        }

        user.RefreshToken = Guid.NewGuid().ToString();

        var jwtToken = _jwtTokenHelper.CreateAccessToken(user, user.RefreshToken);



        await _unitOfWork.UserRepository.UpdateAsync(user);

        await _unitOfWork.CommitAsync();

        return new DataResult<Utilities.Security.Auth.Token>(data: jwtToken, status: ResultStatus.Ok);
    }
}
