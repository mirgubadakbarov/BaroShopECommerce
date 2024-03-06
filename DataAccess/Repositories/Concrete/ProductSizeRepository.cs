using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstarct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class ProductSizeRepository : Repository<ProductSize>, IProductSizeRepository
    {
        private readonly AppDbContext _context;

        public ProductSizeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<ProductSize>> GetProductSizesDetailsAsync(int productId)
        {
            var sizes = await _context.ProductSizes.Where(pc => pc.ProductId == productId).ToListAsync();
            return sizes;
        }


    }
}
