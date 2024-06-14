using ELearningAPI.Application.Contracts;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService lessonService;
        public LessonsController(ILessonService lessonService)
        {
            this.lessonService = lessonService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var courses = lessonService.GetLessonList();

            return Ok(courses);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> GetById(int id)
        {
            var lesson = await lessonService.GetLessonById(id);

            return Ok(lesson);
        }


        [HttpPost]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<ActionResult> PostLesson(CreateLessonDto lessonDto)
        {
            var lessonId = await lessonService.CreateLesson(lessonDto);

            return CreatedAtAction(nameof(GetById), new { id = lessonId }, lessonDto);
        }

        [HttpPut]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateLessonDto updateLessonDto)
        {
            await lessonService.UpdateLesson(id, updateLessonDto);

            return NoContent();
        }


        [HttpDelete]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await lessonService.DeleteLesson(id);

            return NoContent();
        }



    }
}
