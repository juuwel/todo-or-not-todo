using System.Security.Claims;
using UserMicroservice.Domain.Entities;

namespace UserMicroservice.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles, IDictionary<string, dynamic>? customClaims, Guid? tenantId = null);
}
