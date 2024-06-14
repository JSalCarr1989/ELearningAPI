namespace ELearningAPI.Models.ViewModels
{
    public class AvailableLessonViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Source { get; set; } = string.Empty;
        public int ApprovalThreshold { get; set; }
        public int CourseId { get; set; }
        public List<int>? CorrelatedLessonIds { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
