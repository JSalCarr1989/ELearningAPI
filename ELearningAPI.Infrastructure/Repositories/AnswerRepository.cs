using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class AnswerRepository : GenericRepository<StudentAnswer>, IAnswerRepository
    {
        private readonly ELearningContext context;
        public AnswerRepository(ELearningContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<StudentAnswer> GetAnswerByUserAndQuestion(int userId, int questionId)
        {

            var answer = await context.StudentAnswers
              .SingleOrDefaultAsync(a => a.UserId == userId && questionId == questionId);

            if (answer == null) return null;

            return answer;
        }

        public async Task<List<StudentAnswer>> GetAnswersByStudentId(int userId)
        {

            var answers = await context.StudentAnswers.Where(sa => sa.UserId == userId).ToListAsync();

            return answers;
        }

        public async Task<StudentAnswer> GetStudentAnswerById(int id)
        {

            var lesson = await context.StudentAnswers
                .SingleOrDefaultAsync(l => l.Id == id);

            if (lesson == null) return null;

            return lesson;

        }

    }
}
