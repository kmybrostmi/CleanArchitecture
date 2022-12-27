using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Entities.Account;
public class RolePermission:BaseEntity
{
    public Role Role { get; set; }
    public Guid RoleId { get; set; }
    public Permission Permission { get; set; }
    public Guid PermissionId { get; set; }
}

