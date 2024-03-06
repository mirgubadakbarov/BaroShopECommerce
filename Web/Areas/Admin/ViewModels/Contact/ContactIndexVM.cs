using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.Contact
{
    public class ContactIndexVM
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
