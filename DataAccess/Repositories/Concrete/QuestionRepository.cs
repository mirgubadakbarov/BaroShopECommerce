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
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly AppDbContext _context;

        public QuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Question>> FilterByCategory(int? categoryId)
        {
            return await _context.Questions.Where(p => categoryId != null ? p.QuestionCategoryId == categoryId : true).ToListAsync();
        }


        public async Task<List<Question>> GetQuestionsWithCategory()
        {
            var questions = await _context.Questions.Include(ct => ct.QuestionCategory).ToListAsync();
            return questions;
        }
    }
}
