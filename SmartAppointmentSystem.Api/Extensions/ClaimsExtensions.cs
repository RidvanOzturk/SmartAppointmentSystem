using System.Security.Claims;

namespace SmartAppointmentSystem.Api.Extensions;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdValue = user.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userId = Guid.Parse(userIdValue);
        return userId;
    }
 
}
