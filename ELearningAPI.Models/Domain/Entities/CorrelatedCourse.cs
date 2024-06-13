namespace ELearningAPI.Models.Domain.Entities
{
    public class CorrelatedCourse
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid CorrelatedCourseId { get; set; }
        public Course RelatedCourse { get; set; }

    }
}
