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
    public class ProductColorRepository : Repository<ProductColor>, IProductColorRepository
    {
        private readonly AppDbContext _context;

        public ProductColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductColor>> GetProductColorsDetailsAsync(int productId)
        {
            var colors = await _context.ProductColors.Where(pc => pc.ProductId == productId).ToListAsync();
            return colors;
        }
    }
}
