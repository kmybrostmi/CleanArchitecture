namespace CleanArchitecture.Domain.Common;
public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsActived { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreateDate { get; set; } 
    public Guid CreateById { get; set; }
    public DateTime ModifiedDate { get; set; } 
    public Guid ModifiedById { get; set; }

}


