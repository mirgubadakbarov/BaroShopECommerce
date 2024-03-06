using Core.Entities;
using Core.Utilities.FileService;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstarct;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using Web.Services.Abstract;
using Web.Services.Concrete;
using AdminAbstractService = Web.Areas.Admin.Services.Abstract;
using AdminConcreteService = Web.Areas.Admin.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IUrlHelper>(factory =>
{
    var actionContext = factory.GetService<IActionContextAccessor>()
                               .ActionContext;
    return new UrlHelper(actionContext);
});


var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAccess")));

builder.Services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();



builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireUppercase = false;

    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;

    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.AllowedForNewUsers = true;
});


#region Repository
builder.Services.AddScoped<IHomeMainSliderRepository, HomeMainSliderRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ISpecialSliderRepository, SpecialSliderRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IHomeSpecialDayRepository, HomeSpecialDayRepository>();
builder.Services.AddScoped<IHomeSwiperRepository, HomeSwiperRepository>();
builder.Services.AddScoped<IOurServiceRepository, OurServiceRepository>();
builder.Services.AddScoped<ITestimonialRepository, TestimonialRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionCategoryRepository, QuestionCategoryRepository>();
builder.Services.AddScoped<IBusinessInfoRepository, BusinessInfoRepository>();
builder.Services.AddScoped<IFactRepository, FactRepository>();
builder.Services.AddScoped<IWhatWeDoRepository, WhatWeDoRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ISendMessageRepository, SendMessageRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketProductRepository, BasketProductRepository>();

#endregion

#region Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IHomeService, HomeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();






builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddScoped<AdminAbstractService.IAccountService, AdminConcreteService.AccountService>();
builder.Services.AddScoped<AdminAbstractService.IEmailService, AdminConcreteService.EmailService>();
builder.Services.AddScoped<AdminAbstractService.IHomeMainSliderService, AdminConcreteService.HomeMainSliderService>();
builder.Services.AddScoped<AdminAbstractService.IBrandService, AdminConcreteService.BrandService>();
builder.Services.AddScoped<AdminAbstractService.ISpecialSliderService, AdminConcreteService.SpecialSliderService>();
builder.Services.AddScoped<AdminAbstractService.IColorService, AdminConcreteService.ColorService>();
builder.Services.AddScoped<AdminAbstractService.IProductService, AdminConcreteService.ProductService>();
builder.Services.AddScoped<AdminAbstractService.ISizeService, AdminConcreteService.SizeService>();
builder.Services.AddScoped<AdminAbstractService.IHomeSpecialDayService, AdminConcreteService.HomeSpecialDayService>();
builder.Services.AddScoped<AdminAbstractService.IHomeSwiperService, AdminConcreteService.HomeSwiperService>();
builder.Services.AddScoped<AdminAbstractService.IOurServiceService, AdminConcreteService.OurServiceService>();
builder.Services.AddScoped<AdminAbstractService.ITestimonialService, AdminConcreteService.TestimonialService>();
builder.Services.AddScoped<AdminAbstractService.IQuestionService, AdminConcreteService.QuestionService>();
builder.Services.AddScoped<AdminAbstractService.IQuestionCategoryService, AdminConcreteService.QuestionCategoryService>();
builder.Services.AddScoped<AdminAbstractService.IBusinessInfoService, AdminConcreteService.BusinessInfoService>();
builder.Services.AddScoped<AdminAbstractService.IFactService, AdminConcreteService.FactService>();
builder.Services.AddScoped<AdminAbstractService.IWhatWeDoService, AdminConcreteService.WhatWeDoService>();
builder.Services.AddScoped<AdminAbstractService.IServiceService, AdminConcreteService.ServiceService>();
builder.Services.AddScoped<AdminAbstractService.IMessageService, AdminConcreteService.MessageService>();
builder.Services.AddScoped<AdminAbstractService.IContactService, AdminConcreteService.ContactService>();
builder.Services.AddScoped<AdminAbstractService.ILocationService, AdminConcreteService.LocationService>();

#endregion



var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    var _configuration = scope.ServiceProvider.GetService<IConfiguration>();
    // Generate the token

    // Send the email


    await DbInitialize.SeedAsync(userManager, roleManager, _configuration);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();



app.MapControllerRoute(
        name: "areas",
            pattern: "{area:exists}/{controller=Homemainslider}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run();