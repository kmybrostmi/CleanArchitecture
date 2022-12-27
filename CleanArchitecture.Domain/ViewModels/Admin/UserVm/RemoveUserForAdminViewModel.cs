namespace CleanArchitecture.Domain.ViewModels.Admin.UserVm;
public class RemoveUserForAdminViewModel
{
    public Guid userId { get; set; }
    public Guid ModifiedBy { get; set; }
}

public enum RemoveUserForAdminResult
{
    Success,
    failed
}