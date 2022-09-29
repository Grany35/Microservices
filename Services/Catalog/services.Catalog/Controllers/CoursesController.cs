using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.Catalog.Dtos;
using services.Catalog.Services;

namespace services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);

            if (response.StatusCode == 404)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _courseService.GetAllAsync();
            if (response.StatusCode == 404)
            {
                return BadRequest(response.Errors);
            }
            return Ok(response);
        }

        [HttpGet("GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _courseService.GetAllByUserIdAsync(userId);
            if (response.StatusCode == 404)
            {
                return BadRequest(response.Errors);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
        {
            var response = await _courseService.CreateAsync(courseCreateDto);

            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            return BadRequest(response.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CourseUpdateDto courseUpdateDto)
        {
            var response = await _courseService.UpdateAsync(courseUpdateDto);
            if (response.StatusCode == 204)
            {
                return NoContent();
            }
            return BadRequest(response.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _courseService.DeleteAsync(id);
            if (response.StatusCode==204)
            {
                return NoContent();
            }
            return BadRequest(response.Errors);
        }
    }
}
