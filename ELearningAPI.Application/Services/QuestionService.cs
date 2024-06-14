using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IUserContextService userContextService;
        public QuestionService(IQuestionRepository questionRepository, IUserContextService userContextService)
        {
            this.questionRepository = questionRepository;
            this.userContextService = userContextService;
        }

        public int CreateQuestion(CreateQuestionDto questionDto)
        {

            var userId = userContextService.GetUserId();

            var question = new Question
            {
                Description = questionDto.Description,
                Type = questionDto.Type,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow,
                LessonId = questionDto.LessonId,
                Options = questionDto.Options.Select(o => new QuestionOption
                {
                    Option = o.Option,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };



            var newQuestionId = questionRepository.Add(question);


            return newQuestionId;

        }

        public async Task DeleteQuestion(int id)
        {
            var question = await questionRepository.GetById(id);

            if (question == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            questionRepository.Delete(question.Id);
        }

        public async Task<QuestionViewModel> GetQuestionById(int id)
        {

            var question = await questionRepository.GetQuestionById(id);



            var questionToView = MapQuestionToViewModel(question);

            return questionToView;

        }

        public List<QuestionViewModel> GetQuestionList()
        {
            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();

            var questionsFromDb = questionRepository.GetAll();

            foreach (var question in questionsFromDb)
            {
                var viewLesson = MapQuestionToViewModel(question);

                questionViewModels.Add(viewLesson);
            }

            return questionViewModels;
        }

        public async Task UpdateQuestion(int id, UpdateQuestionDto updateDto)
        {
            var question = await questionRepository.GetById(id);

            if (question == null)
            {
                throw new KeyNotFoundException("Lesson not found");
            }

            question.Description = updateDto.Description;
            question.Score = updateDto.Score;
            question.UpdatedAt = DateTime.Now;


            questionRepository.Update(question);
        }

        private QuestionViewModel MapQuestionToViewModel(Question question)
        {
            return new QuestionViewModel
            {
                Id = question.Id,
                Description = question.Description,
                Type = question.Type,
                LessonId = question.LessonId,
                Options = question.Options.Select(o => new QuestionOptionViewModel()
                {
                    Id = o.Id,
                    Option = o.Option,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };
        }
    }
}
