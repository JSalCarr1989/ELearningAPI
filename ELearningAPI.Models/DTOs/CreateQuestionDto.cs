using ELearningAPI.Models.Domain.Enums;

namespace ELearningAPI.Models.DTOs
{
    public class CreateQuestionDto
    {
        public string Description { get; set; }
        public int Score { get; set; }
        public QuestionType Type { get; set; }
        public int LessonId { get; set; }
        public List<CreateQuestionOptionsDto> Options { get; set; }
    }
}
