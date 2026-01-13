using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using OllamaSharp.Models;
using poc.ai.ollama.generative.api.Dto;

namespace poc.ai.ollama.generative.api.Controllers;

[ApiController]
[Route("api/ollama")]
public sealed class OllamaController : ControllerBase
{
    private readonly OllamaApiClient _ollama;

    public OllamaController(OllamaApiClient ollama) =>
        _ollama = ollama;

    [HttpPost("prompt")]
    public async Task<IActionResult> SendPrompt([FromBody] AskToOllamaRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest("Prompt cannot be empty.");

        var response = string.Empty;

        var generateRequest = new GenerateRequest
        {
            Model = "llama3",
            Prompt = request.Prompt
        };

        await foreach (var chunk in _ollama.GenerateAsync(generateRequest))
        {
            response += chunk.Response;

            if (chunk.Done)
                break;
        }

        return Ok(new
        {
            prompt = request.Prompt,
            response
        });
    }
}
