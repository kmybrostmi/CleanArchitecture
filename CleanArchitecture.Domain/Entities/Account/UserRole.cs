using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities.Account;
public class UserRole:BaseEntity
{
    public Users User { get; set; }
    public Guid UserId { get; set; }

    public Role Role { get; set; }
    public Guid RoleId { get; set; }
}