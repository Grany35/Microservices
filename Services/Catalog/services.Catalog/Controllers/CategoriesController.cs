using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using services.Catalog.Dtos;
using services.Catalog.Models;
using services.Catalog.Services;

namespace services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
           
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (response.StatusCode == 404)
            {
                return BadRequest(response.Errors);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);
            if (response.StatusCode==200)
            {
                return Ok(response);
            }
            return BadRequest(response.Errors);
        }
    }
}
