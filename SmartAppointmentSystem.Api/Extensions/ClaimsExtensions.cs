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
    public static string GetName(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "Name")?.Value;
    }
    public static string GetEmail(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(x => x.Type == "Mail")?.Value;
    }
 

}
