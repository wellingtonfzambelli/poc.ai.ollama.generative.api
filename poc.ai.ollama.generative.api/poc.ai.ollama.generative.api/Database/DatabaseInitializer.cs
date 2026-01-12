using Microsoft.Data.Sqlite;

namespace poc.ai.ollama.generative.api.Database;

public static class DatabaseInitializer
{
    public static void Initialize(string connectionString)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        Execute("Database/01_schemas-script.sql", connection);
        Execute("Database/02_inserts-script.sql", connection);
    }

    private static void Execute(string relativePath, SqliteConnection connection)
    {
        var path = Path.Combine(AppContext.BaseDirectory, relativePath);

        if (!File.Exists(path))
            throw new FileNotFoundException($"SQL script not found: {path}");

        var sql = File.ReadAllText(path);

        using var cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }
}