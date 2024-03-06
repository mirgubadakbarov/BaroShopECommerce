using Core.Entities;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstarct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concrete
{
    public class OurServiceRepository : Repository<OurService>, IOurServiceRepository
    {
        public OurServiceRepository(AppDbContext context) : base(context) { }

    }
}
