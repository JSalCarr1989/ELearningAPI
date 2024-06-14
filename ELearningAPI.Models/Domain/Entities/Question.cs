using ELearningAPI.Models.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.Domain.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public QuestionType Type { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public ICollection<QuestionOption> Options { get; set; }



    }
}
