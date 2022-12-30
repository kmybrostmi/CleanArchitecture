
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class DeleteProductViewModel
{
    public Guid ProductId { get; set; }
    public Guid ModifiedBy { get; set; }
}

public enum DeleteProductResult
{
    NotFound,
    Success
}



