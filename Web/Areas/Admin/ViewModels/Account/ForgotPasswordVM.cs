using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required, MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string? Id { get; set; }
        public string? Token { get; set; }
    }
}
