using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.Domain.Enums;

namespace ELearningAPI.Models.ViewModels
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public QuestionType Type { get; set; }
        public Guid LessonId { get; set; }

        public List<QuestionOptionViewModel> Options { get; set; }
    }
}
