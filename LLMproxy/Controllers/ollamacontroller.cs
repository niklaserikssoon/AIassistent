using AIassistent.DTOs;
using AIassistent.services;
using Microsoft.AspNetCore.Mvc;

namespace AIassistent.Controllers
{
    [ApiController]
    [Route("api/ollama")]
    public class Ollamacontroller : ControllerBase
    {

        private readonly IpostService _postService;

        public Ollamacontroller(IpostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<ActionResult<responseDTO>> Post(requestDTO request)
        {
            var result = await _postService.PostAsync(request.Promt);

            var response = new responseDTO
            {
                GeneratedText = result
            };

            return Ok(response);
        }

    }
}
