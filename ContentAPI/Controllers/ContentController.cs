using Microsoft.AspNetCore.Mvc;
using ContentAPI.Services;
using ContentAPI.DTOs;

namespace ContentAPI.Controllers
{
    [ApiController]
    [Route("api/content")]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDTO request)
        {
            var result = await _contentService.CreateAsync(request.Promt, request.Category);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDTO>>> GetAll([FromQuery] string? category)
        {
            var result = await _contentService.GetAllAsync(category);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _contentService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _contentService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateRequestDTO request)
        {
            var result = await _contentService.UpdateAsync(id, request);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}