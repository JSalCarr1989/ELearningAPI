using ELearningAPI.Application.Contracts;
using ELearningAPI.Application.Services;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService answerService;
        public AnswersController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "RequiredStudentRole")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await answerService.GetAnswerById(id);

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Policy = "RequiredStudentRole")]
        public  ActionResult PostQuestion(CreateStudentAnswerDto answerDto)
        {
            var answerId = answerService.CreateAnswer(answerDto);

            return CreatedAtAction(nameof(GetById), new { id = answerId }, answerDto);
        }
    }
}
