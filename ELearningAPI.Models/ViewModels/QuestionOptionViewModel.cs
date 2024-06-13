namespace ELearningAPI.Models.ViewModels
{
    public class QuestionOptionViewModel
    {
        public Guid Id { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
    }
}
