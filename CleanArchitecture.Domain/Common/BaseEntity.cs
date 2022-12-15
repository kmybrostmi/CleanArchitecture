namespace CleanArchitecture.Domain.Common;
public class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsActived { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public Guid CreateById { get; set; }
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    public Guid ModifiedById { get; set; }

}


