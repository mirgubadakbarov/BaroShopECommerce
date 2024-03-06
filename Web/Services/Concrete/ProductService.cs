using Core.Constants;
using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Principal;
using Web.Services.Abstract;
using Web.ViewModels.Components;
using Web.ViewModels.Product;

namespace Web.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IProductRepository productRepository,
            IBrandRepository brandRepository,
            IColorRepository colorRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IQueryable<Product>> FilterByName(string? name)
        {
            var products = await _productRepository.FilterByName(name);
            return products;
        }

        public async Task<ProductIndexVM> GetAllAsync(ProductIndexVM model)
        {
            var queryString = _httpContextAccessor.HttpContext.Request.Query;
            string gender = queryString["genderCheck"];
            string checkModel = queryString["modelCheck"];
            string checkMaterial = queryString["materialCheck"];
            string brendCheck = queryString["brendCheck"];
            string colorCheck = queryString["checkColor"];

            if (gender != null) model.Gender = gender;
            if (checkModel != null) model.Model = checkModel;
            if (checkMaterial != null) model.Material = checkMaterial;
            if (brendCheck != null) model.BrandId = int.Parse(brendCheck);
            if (colorCheck != null) model.ColorId = int.Parse(colorCheck);

            var products = await FilterByName(model.SearchInput);
            products = await _productRepository.FilterByGender(products, model.Gender);
            products = await _productRepository.FilterByModel(products, model.Model);
            products = await _productRepository.FilterByMaterial(products, model.Material);
            products = await _productRepository.FilterByPrice(products, model.MinPrice, model.MaxPrice);
            products = await _productRepository.FilterByBrand(products, model.BrandId);
            products = await _productRepository.FilterByColor(products, model.ColorId);
            var pageCount = await _productRepository.GetPageCountAsync(products, model.Take);
            products = await _productRepository.PaginateProductAsync(products, model.Page, model.Take);
            model = new ProductIndexVM
            {

                Products = products.OrderByDescending(pr => pr.CreatedAt).ToList(),
                Brands = await _brandRepository.GetAllAsync(),
                Colors = await _colorRepository.GetAllAsync(),
                Page = model.Page,
                PageCount = pageCount,
                Take = model.Take,
                SearchInput = model.SearchInput,
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice
            };
            return model;
        }

      

        public async Task<HeaderComponentVM> FilterAllByName(string? name)
        {
            var products = await _productRepository.FilterByName(name);
            var model = new HeaderComponentVM
            {
                Products = products.ToList()
            };
            return model;
        }

        public async Task<ProductDetailsVM> GetDetailsAsync(int id)
        {
            var product = await _productRepository.GetProductDetailsAsync(id);
            var model = new ProductDetailsVM
            {
                Product = product,
                RelatedProducts = await _productRepository.GetRelatedProductsAsync(id, ((int)product.Model), ((int)product.Gender))
            };
            return model;
        }

        public async Task<ProductLoadMoreVM> GetLoadMoreAsync(int skipRow)
        {
            var products = await _productRepository.ProductsLoadMoreAsync(skipRow);
            var model = new ProductLoadMoreVM
            {
                BestSellingProducts = products,
                IsLast = await _productRepository.CheckIsLastAsync(skipRow)
            };
            return model;
        }

     
    }
}
