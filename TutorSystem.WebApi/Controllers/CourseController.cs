using Microsoft.AspNetCore.Mvc;
using TutorSystem.Domain.Dtos;
using TutorSystem.Domain.Features;

namespace TutorSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult GetCourses([FromQuery] string? courseType = null)
        {
            var result = _courseService.GetCourses(courseType);
            return Ok(result); 
        }

        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseCreateRequestDto dto)
        {
            var result = _courseService.CreateCourse(dto);
            if (!result.IsSuccess)
                return BadRequest(result); 

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateRequestDto dto)
        {
            var result = _courseService.UpdateCourse(id, dto);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(int id)
        {
            var result = _courseService.DeleteCourse(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
