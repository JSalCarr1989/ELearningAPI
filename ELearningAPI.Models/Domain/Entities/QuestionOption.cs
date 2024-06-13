using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.Domain.Entities
{
    public class QuestionOption
    {
        [Key]
        public Guid Id { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
