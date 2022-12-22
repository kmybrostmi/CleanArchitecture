using CleanArchitecture.Domain.Entities.Account;

namespace CleanArchitecture.Application.Extensions;
public static class UserExtensions
{
    public static string GetUserName(this Users user)
    {
        return $"{user.FirstName} {user.LastName}";
    }
}
