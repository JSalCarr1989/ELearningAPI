namespace ELearningAPI.Models.ViewModels
{
    public class AvailableCourseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsApproved { get; set; }

        public List<int>? CorrelatedCourseIds { get; set; }
        public List<CorrelatedCourseViewModel>? CorrelatedCourses { get; set; }
        public List<LessonViewModel>? Lessons { get; set; }
    }
}
