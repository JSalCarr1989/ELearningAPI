namespace ELearningAPI.Models.DTOs
{
    public class CreateStudentAnswerDto
    {
        public int QuestionId { get; set; }
        public List<int> SelectedOptions { get; set; }
    }
}
