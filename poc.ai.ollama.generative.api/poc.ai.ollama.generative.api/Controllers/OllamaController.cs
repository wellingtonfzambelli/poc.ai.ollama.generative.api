using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using poc.ai.ollama.generative.api.Dto;

namespace poc.ai.ollama.generative.api.Controllers;

[ApiController]
[Route("api/ollama")]
public sealed class OllamaController : ControllerBase
{
    private readonly OllamaApiClient _ollamaClient;

    public OllamaController(OllamaApiClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    [HttpPost("prompt")]
    public async Task<IActionResult> SendPrompt([FromBody] AskToOllamaRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return BadRequest("Prompt cannot be empty.");

        var messages = new List<OllamaSharp.Models.Chat.ChatMessage>
        {
            new OllamaSharp.Models.Chat.ChatMessage(
                OllamaSharp.Models.Chat.ChatRole.System,
                "You are a helpful assistant."
            ),
            new OllamaSharp.Models.Chat.ChatMessage(
                OllamaSharp.Models.Chat.ChatRole.User,
                request.Prompt
            )
        };

        var response = string.Empty;

        await foreach (var chunk in _ollamaClient.ChatAsync(
            new OllamaSharp.Models.Chat.ChatRequest
            {
                Model = "llama3",
                Messages = messages
            }))
        {
            response += chunk.Message?.Content;
        }

        return Ok(new
        {
            prompt = request.Prompt,
            response
        });
    }
}
