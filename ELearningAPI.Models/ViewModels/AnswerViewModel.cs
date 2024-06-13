namespace ELearningAPI.Models.ViewModels
{
    public class StudentAnswerViewModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid QuestionId { get; set; }

        public List<Guid> SelectedOptions { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
