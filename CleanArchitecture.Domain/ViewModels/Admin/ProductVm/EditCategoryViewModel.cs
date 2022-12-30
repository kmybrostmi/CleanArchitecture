using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.ViewModels.Admin.ProductVm;

public class EditCategoryViewModel : CreateCategoryViewModel
{
    public Guid CategoryId { get; set; }
    public Guid ModifiedBy { get; set; }
    public string ImageName { get; set; }
    public IFormFile CategoryImage { get; set; }

}
public enum EditProductCategoryResult
{
    IsExistUrlName,
    NotFound,
    Success
}


