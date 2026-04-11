using AIassistent.DTOs;
using AIassistent.services;
using ContentAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AIassistent.Controllers
{
    [ApiController]
    [Route("api/ollama")]
    // detta filter körs innan requesten går vidare till action metoden, och kontrollerar att en giltig API-nyckel finns i requesten
    [ServiceFilter(typeof(ApiKeyFilter))]
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
