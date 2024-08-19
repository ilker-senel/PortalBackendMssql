using Business.Utilities.Security.Auth.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Utilities.Security.Auth
{
    public class ClaimHelper : IClaimHelper
    {
        private readonly IEnumerable<Claim> _claims;

        public ClaimHelper(IHttpContextAccessor httpContextAccessor)
        {
            _claims = httpContextAccessor.HttpContext?.User?.Claims ?? new List<Claim>();
        }

        public string? GetUserId()
        {
            var guid = _claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return guid;

        }

        public string? GetUserType()
        {
            var userType = _claims.FirstOrDefault(c => c.Type == ClaimTypes.Actor)?.Value;

            return userType;


        }

        public string? GetClaimByType(string claimType)
        {
            return _claims.FirstOrDefault(c => c.Type == claimType)?.Value;
        }
    }
}
