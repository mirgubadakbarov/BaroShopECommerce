using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Contact : BaseEntity
    {
        [Required]
        public string Address { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string MobileNumber { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Email { get; set; }

    }
}
