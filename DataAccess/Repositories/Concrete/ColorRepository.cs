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
    public class ColorRepository : Repository<Color>, IColorRepository
    {
        private readonly AppDbContext _context;

        public ColorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Color>> GetById(int colorId)
        {
            var colors = await _context.Colors.Where(c => c.Id == colorId).ToListAsync();
            return colors;
        }
    }
}
