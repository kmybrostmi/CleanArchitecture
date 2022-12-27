using CleanArchitecture.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitecture.Domain.Entities.Account;

public class Permission : BaseEntity
{
    [Display(Name = "عنوان نقش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

    public string Title { get; set; }
    public Guid? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public ICollection<Permission> Permissions { get; set; }
    public ICollection<RolePermission> RolePermissions { get; set; }
}