using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public Core.Entities.Product Product { get; set; }

    }
}
