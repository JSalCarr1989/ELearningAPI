namespace ELearningAPI.Models.Domain.Entities
{
    public class CorrelatedCourse
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int CorrelatedCourseId { get; set; }
        public Course RelatedCourse { get; set; }

    }
}
