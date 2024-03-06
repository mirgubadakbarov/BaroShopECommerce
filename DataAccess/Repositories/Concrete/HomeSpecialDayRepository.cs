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
    public class HomeSpecialDayRepository : Repository<HomeSpecialDay>, IHomeSpecialDayRepository
    {
        private readonly AppDbContext _context;

        public HomeSpecialDayRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<HomeSpecialDay> GetHomeSpecialDay()
        {
            var homeSpecialDay = await _context.homeSpecialDay.FirstOrDefaultAsync();
            return homeSpecialDay;

        }
    }
}
