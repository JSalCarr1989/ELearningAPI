using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly ELearningContext context;
        public QuestionRepository(ELearningContext context) : base(context)
        {
            this.context = context;
        }

        public async  Task<Question> GetQuestionById(Guid id)
        {
            var question = await context.Questions
               .Include(l => l.Options)
               .SingleOrDefaultAsync(l => l.Id == id);

            if (question == null) return null;

            return question;
        }
    }
}
