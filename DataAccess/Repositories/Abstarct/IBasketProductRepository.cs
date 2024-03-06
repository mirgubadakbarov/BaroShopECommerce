using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IBasketProductRepository : IRepository<BasketProduct>
    {
        Task<BasketProduct> GetBasketProducts(int modelId, int basketId);
        Task<bool> DeleteProductAsync(int productdId);
        Task<int> GetUserBasketProductsCount(ClaimsPrincipal user);

    }
}
