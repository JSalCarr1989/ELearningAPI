namespace ELearningAPI.Models.Domain.Entities
{
    public class CorrelatedLesson
    {
        public int LessonId { get; set; }

        public int CorrelatedLessonId { get; set; }

        public Lesson Lesson { get; set; }
        public Lesson RelatedLesson { get; set; }

    }
}
