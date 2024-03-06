using Core.Constants;
using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstarct;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsBestSellingAsync()
        {
            var products = await _context.Products
              .Where(pr => pr.BestSelling == true)
              .OrderByDescending(pr => pr.CreatedAt)
              .Take(5)
              .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsInSaleAsync()
        {
            var products = await _context.Products
                .Where(pr => pr.InSale == true)
                .Take(5)
                .OrderByDescending(pr => pr.CreatedAt)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetAllWithBrandAsync()
        {
            var product = await _context.Products
                .OrderByDescending(pr => pr.CreatedAt)
                .Include(br => br.Brand).ToListAsync();
            return product;
        }

        public async Task<Product> GetProductDetailsAsync(int id)
        {
            var product = await _context.Products.Include(br => br.Brand)
                .Include(ph => ph.ProductPhotos)
                .Include(c => c.Colors)
                .ThenInclude(c => c.Color)
                .Include(s => s.Sizes)
                .ThenInclude(s => s.Size)
                .FirstOrDefaultAsync(pr => pr.Id == id);
            return product;
        }
        public async Task<IQueryable<Product>> PaginateProductAsync(IQueryable<Product> products, int page, int take)
        {

            return products
                .OrderByDescending(d => d.CreatedAt)
                .Skip((page - 1) * take)
                .Take(take);
        }

        public async Task<int> GetPageCountAsync(IQueryable<Product> products, int take)
        {
            var pageCount = await products.CountAsync();
            return (int)Math.Ceiling((decimal)pageCount / take);
        }

        public async Task<IQueryable<Product>> FilterByName(string? name)
        {
            return _context.Products
                .Include(pr => pr.Colors)
                .Include(pr => pr.Sizes)
                .Include(pr => pr.Brand)
                .Where(pr => !string.IsNullOrEmpty(name) ? pr.Title.ToLower().Contains(name.ToLower()) : true);
        }

        public async Task<List<Product>> GetRelatedProductsAsync(int productId, int model, int gender)
        {
            var relatedProducts = await _context.Products
                .Include(pr => pr.Brand)
                .Where(pr => ((int)pr.Model) == model && ((int)pr.Gender) == gender && productId != pr.Id)
                .Take(12)
                .ToListAsync();
            return relatedProducts;
        }

        public async Task<Product> GetUpdateModelAsync(int id)
        {
            var product = await _context.Products
                .Include(pr => pr.Brand)
                .Include(pr => pr.Colors)
                .Include(pr => pr.Sizes)
                .Include(ph => ph.ProductPhotos)
                .FirstOrDefaultAsync(pr => pr.Id == id);
            return product;
        }

        public async Task<List<Product>> ProductsLoadMoreAsync(int skipRow)
        {
            var products = await _context.Products
                .OrderByDescending(p => p.CreatedAt)
                .Include(pr => pr.Brand)
                .Where(pr => pr.BestSelling)
                .Skip(5)
                .Take(5)
                .ToListAsync();

            return products;

        }

        public async Task<bool> CheckIsLastAsync(int skipRow)
        {
            if (((skipRow + 1) * 6) + 1 >= _context.Products.Where(pr => pr.BestSelling).Count())
            {
                return true;
            }
            return false;
        }

        public async Task<IQueryable<Product>> FilterByGender(IQueryable<Product> products, string? gender)
        {
            Gender myFilter;
            if (Enum.TryParse(gender, out myFilter))
            {
                return products.Where(x => !string.IsNullOrEmpty(gender) ? x.Gender == myFilter : true);
            }
            return products;
        }

        public async Task<IQueryable<Product>> FilterByModel(IQueryable<Product> products, string? model)
        {
            Model myFilter;
            if (Enum.TryParse<Model>(model, out myFilter) && myFilter != default(Model))
            {
                return products.Where(x => !string.IsNullOrEmpty(model) ? x.Model == myFilter : true); ;
            }
            return products;
        }


        public async Task<IQueryable<Product>> FilterByPrice(IQueryable<Product> products, double? minPrice, double? maxPrice)
        {
            return products
                .Where(p => (minPrice != null ? p.Price >= minPrice : true) && (maxPrice != null ? p.Price <= maxPrice : true));
        }

        public async Task<IQueryable<Product>> FilterByMaterial(IQueryable<Product> products, string? material)
        {
            Material myFilter;
            if (Enum.TryParse<Material>(material, out myFilter) && myFilter != default(Material))
            {
                return products.Where(x => !string.IsNullOrEmpty(material) ? x.Material == myFilter : true); ;
            }
            return products;
        }

        public async Task<IQueryable<Product>> FilterByBrand(IQueryable<Product> products, int brandId)
        {
            return products.
                 Where(pr => brandId != 0 ? pr.BrandId == brandId : true);
        }

        //public async Task<IQueryable<Product>> FilterByColor(IQueryable<Product> products, int colorId)
        //{
        //    if (colorId != 0)
        //    {
        //        var filteredItems = _context.ProductColors.Where(c => c.ColorId == colorId);
        //        return products = (IQueryable<Product>)filteredItems;
        //    }
        //  .Include(m => m.Roles.Where(r => roles.Contains(r.Name)))
        //}

        public async Task<IQueryable<Product>> FilterByColor(IQueryable<Product> products, int colorId)
        {
            if (colorId != 0)
            {
                var a = products.Where(m => m.Colors.Any(r => colorId == r.ColorId));
                return a;

            }
            return products;
        }

    }
}
