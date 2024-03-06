using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetBasketWithProducts(string userId);
    }
}
