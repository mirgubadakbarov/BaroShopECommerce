using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.Contact
{
    public class ContactUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.PhoneNumber), Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
