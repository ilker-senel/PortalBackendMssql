namespace Business.Utilities.Validation.Interface
{
    public interface IValidationHelper
    {
        Task<string> ValidateAsync(dynamic dto);
    }
}
