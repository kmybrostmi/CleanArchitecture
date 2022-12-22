using System.Security.Claims;
using System.Security.Principal;

namespace CleanArchitecture.Application.Extensions;

public static class IdentityExtentions
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        if (claims != null)
        {
            var data = claims.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (data != null) return Guid.Parse(data.Value);
        }

        return default;
    }

    public static Guid GetUserId(this IPrincipal principal)
    {
        var user = (ClaimsPrincipal)principal;

        return user.GetUserId();
    }
}
