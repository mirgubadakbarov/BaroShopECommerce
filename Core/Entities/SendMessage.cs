using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SendMessage : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(40)]
        public string Subject { get; set; }
        [Required, MaxLength(400)]
        public string Message { get; set; }
        public bool IsSend { get; set; }
    }
}
