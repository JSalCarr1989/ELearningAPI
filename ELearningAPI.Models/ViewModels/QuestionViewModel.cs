using ELearningAPI.Models.Domain.Enums;

namespace ELearningAPI.Models.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public QuestionType Type { get; set; }
        public int LessonId { get; set; }

        public List<QuestionOptionViewModel> Options { get; set; }
    }
}
