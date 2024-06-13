namespace ELearningAPI.Models.DTOs
{
    public class UpdateLessonDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public int ApprovalThreshold { get; set; }
    }
}
