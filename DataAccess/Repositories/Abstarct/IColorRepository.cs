using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IColorRepository:IRepository<Color>
    {
        Task<List<Color>> GetById(int colorId);
    }
}
