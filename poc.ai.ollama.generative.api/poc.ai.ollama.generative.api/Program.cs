using Microsoft.EntityFrameworkCore;
using OllamaSharp;
using poc.ai.ollama.generative.api.Context;
using poc.ai.ollama.generative.api.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// SQL LITE
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")!));


// Add Ollama
builder.Services.AddChatClient(
    new OllamaApiClient("http://localhost:11434", "llama3")
);

var app = builder.Build();


// Add Initializer
DatabaseInitializer.Initialize(
    builder.Configuration.GetConnectionString("Sqlite")!
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();