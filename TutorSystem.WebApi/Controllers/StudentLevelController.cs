using Microsoft.AspNetCore.Mvc;
using TutorSystem.Domain.Dtos;
using TutorSystem.Domain.Features;

namespace TutorSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLevelController : ControllerBase
    {
        private readonly StudentLevelService _levelService;

        public StudentLevelController(StudentLevelService levelService)
        {
            _levelService = levelService;
        }

        [HttpGet]
        public IActionResult GetLevels([FromQuery] int? courseId = null)
        {
            var result = _levelService.GetLevels(courseId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateLevel([FromBody] StudentLevelCreateRequestDto dto)
        {
            var result = _levelService.CreateLevel(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateLevel(int id, [FromBody] StudentLevelUpdateRequestDto dto)
        {
            var result = _levelService.UpdateLevel(id, dto);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLevel(int id)
        {
            var result = _levelService.DeleteLevel(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}
