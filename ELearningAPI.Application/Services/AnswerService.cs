using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;
using System.Text.Json;

namespace ELearningAPI.Application.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IAnswerRepository answerRepository;
        private readonly IUserContextService userContextService;
        public AnswerService(IUserContextService userContextService,IAnswerRepository answerRepository)
        {
            this.userContextService = userContextService;
            this.answerRepository = answerRepository;
        }

        public List<int> CreateAnswers(List<CreateStudentAnswerDto> answerDTOs)
        {
            List<int> createdAnswers = new List<int>();

            var userId = userContextService.GetUserId();

            foreach (var answer in answerDTOs)
            {

                StudentAnswer studentAnswer = new StudentAnswer()
                {
                    UserId = userId,
                    QuestionId = answer.QuestionId,
                    SelectedOptions = JsonSerializer.Serialize(answer.SelectedOptions)
                };

                var newAnswer = answerRepository.Add(studentAnswer);

                createdAnswers.Add(newAnswer);

            }

            return createdAnswers;

        }

        public async Task<StudentAnswerViewModel> GetAnswerById(int id)
        {

            var answerFromDb = await answerRepository.GetStudentAnswerById(id);

            var viewAnswer = MapAnswerToViewModel(answerFromDb);

            return viewAnswer;

        }


        private StudentAnswerViewModel MapAnswerToViewModel(StudentAnswer answer)
        {
            return new StudentAnswerViewModel
            {
                Id = answer.Id,
                QuestionId= answer.QuestionId,
                SelectedOptions = JsonSerializer.Deserialize<List<int>>(answer.SelectedOptions)
            };
        }
    }
}
