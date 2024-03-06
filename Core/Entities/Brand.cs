using Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Brand : BaseEntity
    {
        public string Title { get; set; }
        public string PhotoName { get; set; }
        public List<Product> Products { get; set; }

    }
}
