using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CleanArchitecture.Domain.Entities.Account;

public enum Gender
{
    [Display(Name = "آقا")]
    Male,
    [Display(Name = "خانم")]
    Female,
    [Display(Name = "نامشخص")]
    Unknown
}