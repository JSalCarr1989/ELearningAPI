using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.Domain.Entities
{
    public class StudentAnswer
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; }

        public string SelectedOptions { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
