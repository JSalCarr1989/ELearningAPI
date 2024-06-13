namespace ELearningAPI.Models.DTOs
{
    public class CreateCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Guid>? CorrelatedCourseIds { get; set; }

    }
}
