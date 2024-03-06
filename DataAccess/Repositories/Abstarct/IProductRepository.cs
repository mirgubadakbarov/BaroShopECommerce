using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllWithBrandAsync();
        Task<Product> GetProductDetailsAsync(int id);
        Task<List<Product>> GetProductsBestSellingAsync();
        Task<List<Product>> GetProductsInSaleAsync();
        Task<IQueryable<Product>> PaginateProductAsync(IQueryable<Product> products, int page, int take);
        Task<int> GetPageCountAsync(IQueryable<Product> products, int take);
        Task<IQueryable<Product>> FilterByName(string? name);
        Task<IQueryable<Product>> FilterByGender(IQueryable<Product> products, string? gender);
        Task<IQueryable<Product>> FilterByModel(IQueryable<Product> products, string? model);
        Task<IQueryable<Product>> FilterByMaterial(IQueryable<Product> products, string? material);
        Task<IQueryable<Product>> FilterByColor(IQueryable<Product> products, int colorId);
        Task<IQueryable<Product>> FilterByBrand(IQueryable<Product> products, int brandId);
        Task<List<Product>> GetRelatedProductsAsync(int productId, int model, int gender);
        Task<Product> GetUpdateModelAsync(int id);
        Task<List<Product>> ProductsLoadMoreAsync(int skipRow);
        Task<bool> CheckIsLastAsync(int skiprow);
        Task<IQueryable<Product>> FilterByPrice(IQueryable<Product> products, double? minPrice, double? maxPrice);





    }
}
