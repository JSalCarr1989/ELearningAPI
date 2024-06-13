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

        public Guid CreateAnswer(CreateStudentAnswerDto answerDTO)
        {
            var userId = userContextService.GetUserId();

            StudentAnswer studentAnswer = new StudentAnswer()
            {
                UserId = userId,
                QuestionId = answerDTO.QuestionId,
                SelectedOptions = JsonSerializer.Serialize(answerDTO.SelectedOptions)
            };

            var newAnswer = answerRepository.Add(studentAnswer);

            return newAnswer;

        }

        public async Task<StudentAnswerViewModel> GetAnswerById(Guid id)
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
                SelectedOptions = JsonSerializer.Deserialize<List<Guid>>(answer.SelectedOptions)
            };
        }
    }
}
