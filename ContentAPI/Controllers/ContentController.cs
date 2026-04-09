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
        public async Task<IActionResult> CreateAsync([FromBody] CreateRequestDTO request, [FromServices] IContentService contentService)
        {
            var result = await contentService.CreateAsync(request.Promt, request.Category);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromServices] IContentService contentService)
        {
            var result = await contentService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, [FromServices] IContentService contentService)
        {
            var result = await contentService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id, [FromServices] IContentService contentService)
        {
            var result = await contentService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateRequestDTO request, [FromServices] IContentService contentService)
        {
            var result = await contentService.UpdateAsync(id, request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
