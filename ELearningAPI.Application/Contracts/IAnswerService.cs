using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface IAnswerService
    {
        List<int> CreateAnswers(List<CreateStudentAnswerDto> answerDTOs);

        Task<StudentAnswerViewModel> GetAnswerById(int id);

    }
}
