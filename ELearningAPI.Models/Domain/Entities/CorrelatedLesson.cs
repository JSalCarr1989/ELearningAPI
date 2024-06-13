namespace ELearningAPI.Models.Domain.Entities
{
    public class CorrelatedLesson
    {
        public Guid LessonId { get; set; }

        public Guid CorrelatedLessonId { get; set; }

        public Lesson Lesson { get; set; }
        public Lesson RelatedLesson { get; set; }

    }
}
