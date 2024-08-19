namespace Business.Utilities.Security.Auth.Interface
{
    public interface IClaimHelper
    {
        int? GetUserId();
        string? GetUserType();
        string? GetClaimByType(string claimType);
    }
}
