using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface IQuestionService
    {
        int CreateQuestion(CreateQuestionDto questionDto);

        Task<QuestionViewModel> GetQuestionById(int id);

        List<QuestionViewModel> GetQuestionList();

        Task UpdateQuestion(int id, UpdateQuestionDto updateDto);

        Task DeleteQuestion(int id);


    }
}
