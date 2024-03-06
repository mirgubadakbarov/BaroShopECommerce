using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Web.Areas.Admin.ViewModels.Contact
{
    public class ContactCreateVM
    {
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.PhoneNumber), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
