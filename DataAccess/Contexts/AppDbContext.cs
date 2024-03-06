using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HomeMainSlider> HomeMainSlider { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<SpecialSlider> SpecialSlider { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<HomeSpecialDay> homeSpecialDay { get; set; }
        public DbSet<HomeSwiper> HomeSwipers { get; set; }
        public DbSet<OurService> OurServices { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionCategory> QuestionsCategories { get; set; }
        public DbSet<BusinessInfo> BusinessInfos { get; set; }
        public DbSet<Fact> Facts { get; set; }
        public DbSet<WhatWedo> WhatWedo { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SendMessage> SendMessages { get; set; }
        public DbSet<Contact> Contact { get; set; }

        public DbSet<Location> Location { get; set; }





    }
}
