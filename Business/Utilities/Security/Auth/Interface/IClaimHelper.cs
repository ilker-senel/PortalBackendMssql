namespace Business.Utilities.Security.Auth.Interface
{
    public interface IClaimHelper
    {
        string? GetUserId();
        string? GetUserType();
        string? GetClaimByType(string claimType);
    }
}
