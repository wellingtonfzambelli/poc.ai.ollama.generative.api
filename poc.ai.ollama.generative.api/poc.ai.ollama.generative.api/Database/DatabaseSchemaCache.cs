namespace poc.ai.ollama.generative.api.Database;

public static class DatabaseSchemaCache
{
    private static readonly Lazy<string> _schema = new(LoadSchema);

    public static string GetSchema() => _schema.Value;

    private static string? LoadSchema()
    {
        var path = Path.Combine(
            AppContext.BaseDirectory,
            "Database",
            "01_schemas-script.sql"
        );

        if (!File.Exists(path))
            return null;

        return File.ReadAllText(path);
    }
}