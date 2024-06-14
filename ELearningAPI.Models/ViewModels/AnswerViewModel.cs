namespace ELearningAPI.Models.ViewModels
{
    public class StudentAnswerViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public List<int> SelectedOptions { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
