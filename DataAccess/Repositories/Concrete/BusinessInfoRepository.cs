using Core.Entities;
using Core.Entities.Base;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class BusinessInfoRepository : Repository<BusinessInfo>, IBusinessInfoRepository
    {
        public BusinessInfoRepository(AppDbContext context) : base(context) { }

    }
}
