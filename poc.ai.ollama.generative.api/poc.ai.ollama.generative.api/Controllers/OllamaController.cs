using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using poc.ai.ollama.generative.api.Context;
using poc.ai.ollama.generative.api.Database;
using poc.ai.ollama.generative.api.Dto;
using poc.ai.ollama.generative.api.Helpers;
using System.Text.Json;

namespace poc.ai.ollama.generative.api.Controllers;

[ApiController]
[Route("api/ollama")]
public sealed class OllamaController : ControllerBase
{    
    private readonly IChatClient _chatClient;

    public OllamaController(IChatClient chatClient) =>
        _chatClient = chatClient;

    [HttpPost("ask-to-ollama")]
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

    [HttpPost("retrieve-infos")]
    public async Task<IActionResult> RetrieveInfos
    (
        [FromBody] AskToOllamaRequestDto request,
        [FromServices] AppDbContext dbContext
    )
    {
        // Load schema from cache
        string schema = DatabaseSchemaCache.GetSchema();

        // Generate SQL
        var sqlMessages = new List<ChatMessage>
        {
            new(ChatRole.System,
                """
                You are an assistant that translates English questions into SQL queries.

                Rules:
                - Return ONLY the SQL query
                - Only SELECT statements are allowed
                - Use ONLY the provided database schema
                - If the question asks for multiple metrics, return them in a single query
                - Always use explicit column aliases
                - Column and table names must match the schema exactly (case-sensitive)
                - Use ISO date format (YYYY-MM-DD) for date filters
                """ + schema),
            new(ChatRole.User, request.Prompt)
        };

        var sqlResponse = await _chatClient.GetResponseAsync(sqlMessages);
        var sql = sqlResponse.Text.NormalizeSql().SanitizeSql();

        Console.WriteLine($"Generated SQL: {sql}");

        if (!IsSafeSelectQuery(sql))
            return BadRequest("Generated SQL is not allowed.");

        // Execute SQL → JSON
        var jsonResult = await ExecuteSqlAsJsonAsync(dbContext, sql);

        // Ask Ollama to interpret JSON
        var explainMessages = new List<ChatMessage>
        {
            new(ChatRole.System,
                """
                You are a professional assistant that converts database query results into concise,
                objective English answers.

                Rules:
                - Be brief and factual
                - Do NOT add compliments, opinions, or suggestions
                - Do NOT ask follow-up questions
                - Do NOT add extra context
                - Answer only what was asked
                """),
            new(ChatRole.User,
                $"""
                Question:
                {request.Prompt}

                Query result (JSON):
                {jsonResult}
                """)
        };

        var finalResponse = await _chatClient.GetResponseAsync(explainMessages);

        return Ok(finalResponse.Text);
    }

    private static async Task<string> ExecuteSqlAsJsonAsync(DbContext dbContext, string sql)
    {
        await using var connection = dbContext.Database.GetDbConnection();
        await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = sql;

        await using var reader = await command.ExecuteReaderAsync();

        var rows = new List<Dictionary<string, object?>>();

        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] =
                    reader.IsDBNull(i) ? null : reader.GetValue(i);
            }

            rows.Add(row);
        }

        return JsonSerializer.Serialize(rows);
    }

    private static bool IsSafeSelectQuery(string sql)
    {
        if (string.IsNullOrWhiteSpace(sql))
            return false;

        var normalized = sql.Trim().ToLowerInvariant();

        return normalized.StartsWith("select")
            && !normalized.Contains("insert")
            && !normalized.Contains("update")
            && !normalized.Contains("delete")
            && !normalized.Contains("drop")
            && !normalized.Contains("alter")
            && !normalized.Contains("truncate");
    }
}