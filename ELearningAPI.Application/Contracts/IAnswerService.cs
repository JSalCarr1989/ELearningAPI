using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface IAnswerService
    {
        Guid CreateAnswer(CreateStudentAnswerDto answerDTO);

        Task<StudentAnswerViewModel> GetAnswerById(Guid id);

    }
}
