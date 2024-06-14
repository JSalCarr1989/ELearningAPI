using System.ComponentModel.DataAnnotations;

namespace ELearningAPI.Models.Domain.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int ApprovalThreshold { get; set; }
        public int CourseId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Course Course { get; set; }
        public ICollection<CorrelatedLesson> CorrelatedLessons { get; set; }

        public ICollection<Question> Questions { get; set; }


    }
}
