using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;

using poc.ai.ollama.generative.api.Dto;

namespace poc.ai.ollama.generative.api.Controllers;

[ApiController]
[Route("api/ollama")]
public sealed class OllamaController : ControllerBase
{    
    private readonly IChatClient _chatClient;

    public OllamaController(IChatClient chatClient) =>
        _chatClient = chatClient;

    [HttpPost("prompt")]
    public async Task<IActionResult> SendPrompt([FromBody] AskToOllamaRequestDto request)
    {
        IEnumerable<ChatMessage> messages = new List<ChatMessage>
        {
            new ChatMessage(ChatRole.System, "You are a helpful assistant."),
            new ChatMessage(ChatRole.User, request.Prompt)
        };

        var result = await _chatClient.GetResponseAsync(messages);

        return Ok(result.Text);
    }
}