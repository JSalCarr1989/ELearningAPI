namespace ELearningAPI.Models.DTOs
{
    public class CreateStudentAnswerDto
    {
        public Guid QuestionId { get; set; }
        public List<Guid> SelectedOptions { get; set; }
    }
}
