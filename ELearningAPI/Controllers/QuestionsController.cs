using ELearningAPI.Application.Contracts;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionsController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var questions = questionService.GetQuestionList();

            return Ok(questions);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await questionService.GetQuestionById(id);

            return Ok(question);
        }


        [HttpPost]
        [Authorize(Policy = "RequireProfessorRole")]
        public ActionResult PostQuestion(CreateQuestionDto questionDto)
        {
            var lessonId = questionService.CreateQuestion(questionDto);

            return CreatedAtAction(nameof(GetById), new { id = lessonId }, questionDto);
        }

        [HttpPut]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionDto updateQuestionDto)
        {
            await questionService.UpdateQuestion(id, updateQuestionDto);

            return NoContent();
        }


        [HttpDelete]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await questionService.DeleteQuestion(id);

            return NoContent();
        }

    }
}
