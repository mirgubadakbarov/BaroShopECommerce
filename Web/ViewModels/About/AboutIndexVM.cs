using Core.Entities;

namespace Web.ViewModels.About
{
    public class AboutIndexVM
    {
        public List<BusinessInfo> BusinessInfos { get; set; }
        public List<Fact> Facts { get; set; }

        public WhatWedo WhatWedo { get; set; }
        public List<Service> Services { get; set; }
        public SendMessage Message { get; set; }
        public Contact ContactInfo { get; set; }
        public Location Map { get; set; }
        public bool IsSent { get; set; }

    }
}
