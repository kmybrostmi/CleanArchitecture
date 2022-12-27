using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities.Account;

public class Role : BaseEntity
{
    [Display(Name = "عنوان نقش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string RoleTitle { get; set; }


    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}

