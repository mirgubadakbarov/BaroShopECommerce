using Core.Entities;
using DataAccess.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IProductSizeRepository : IRepository<ProductSize>
    {
        Task<List<ProductSize>> GetProductSizesDetailsAsync(int productId);
    }
}
