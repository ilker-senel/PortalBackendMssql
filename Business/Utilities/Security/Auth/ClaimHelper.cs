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

        public int? GetUserId()
        {
            if (int.TryParse(_claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out var id))
            {
                return id;
            }

            return null;
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
