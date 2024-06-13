using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface IQuestionService
    {
        Guid CreateQuestion(CreateQuestionDto questionDto);

        Task<QuestionViewModel> GetQuestionById(Guid id);

        List<QuestionViewModel> GetQuestionList();

        Task UpdateQuestion(Guid id, UpdateQuestionDto updateDto);

        Task DeleteQuestion(Guid id);


    }
}
