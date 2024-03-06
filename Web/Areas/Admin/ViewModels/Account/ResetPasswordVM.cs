using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.Account
{
    public class ResetPasswordVM
    {
        public string? Id { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password), Display(Name = "Password")]
        public string Password { get; set; }
        [Required, MaxLength(20), DataType(DataType.Password), Display(Name = "Confirm Password"), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string? Token { get; set; }
    }
}
