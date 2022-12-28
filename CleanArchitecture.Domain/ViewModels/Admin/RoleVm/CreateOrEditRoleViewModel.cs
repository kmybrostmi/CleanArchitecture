using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.ViewModels.Admin.RoleVm;
public class CreateOrEditRoleViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "عنوان نقش")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string RoleTitle { get; set; }

    public List<Guid> SelectedPermissions { get; set; }

    public Guid CreateBy { get; set; }
    public Guid ModifiedBy { get; set; }
}
public enum CreateOrEditRoleResult
{
    NotFound,
    Success,
    NotExistPermissions
}


