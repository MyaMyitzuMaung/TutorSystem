using Microsoft.AspNetCore.Mvc;
using TutorSystem.Domain.Dtos;
using TutorSystem.Domain.Features;

namespace TutorSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly TeacherService _teacherService;

        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/Teacher?courseId=1&expertise=Math
        [HttpGet]
        public IActionResult GetTeachers([FromQuery] int? courseId = null, [FromQuery] string? expertise = null)
        {
            var result = _teacherService.GetTeachers(courseId, expertise);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] TeacherCreateRequestDto dto)
        {
            var result = _teacherService.CreateTeacher(dto);
            if (!result.IsSuccess)
                return BadRequest(result); 

            return Ok(result); 
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] TeacherUpdateRequestDto dto)
        {
            var result = _teacherService.UpdateTeacher(id, dto);
            if (!result.IsSuccess)
                return NotFound(result); 
            return Ok(result); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            var result = _teacherService.DeleteTeacher(id);
            if (!result.IsSuccess)
                return NotFound(result); 

            return Ok(result); 
        }
    }
}
