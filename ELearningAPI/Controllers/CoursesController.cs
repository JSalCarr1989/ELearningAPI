using ELearningAPI.Application.Contracts;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var courses = courseService.GetCourseList();

            return Ok(courses);
        }

        [HttpGet("Available/{id}")]
        public async Task<IActionResult> GetAvailableCourses(int id)
        {
            var courses = await courseService.GetAvailableCourses(id);

            return Ok(courses);

        }





        [HttpGet("{id}")]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> GetById(int id)
        {
            var course = await courseService.GetCourseById(id);

            return Ok(course);
        }


        [HttpPost]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<ActionResult> PostCreateCourse([FromBody] CreateCourseDto course)
        {
            

            var courseId = await courseService.CreateCourse(course);

            return CreatedAtAction(nameof(GetById), new { id = courseId },course);
        }

        [HttpPut]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourse)
        {
            await courseService.UpdateCourse(id,updateCourse);

            return NoContent();
        }


        [HttpDelete]
        [Authorize(Policy = "RequireProfessorRole")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await courseService.DeleteCourse(id);

            return NoContent();
        }

    }
}
