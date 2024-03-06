﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.QuestionCategory
{
    public class QuestionCategoryCreateVM
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Display(Name = "Active Status")]
        public bool ActiveStatus { get; set; }
    }
}
