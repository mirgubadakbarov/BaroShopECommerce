using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Abstarct
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<List<Question>> GetQuestionsWithCategory();

        Task<List<Question>> FilterByCategory(int? categoryId);
    }
}
