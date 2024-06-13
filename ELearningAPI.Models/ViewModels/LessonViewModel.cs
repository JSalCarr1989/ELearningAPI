namespace ELearningAPI.Models.ViewModels
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Source { get; set; } = string.Empty;
        public int ApprovalThreshold { get; set; }
        public Guid CourseId { get; set; }
        public List<Guid>? CorrelatedLessonIds { get; set; }
        public List<QuestionViewModel> Questions { get; set; }

    }
}
