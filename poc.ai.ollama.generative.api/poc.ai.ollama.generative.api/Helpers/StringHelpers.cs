using System.Text.RegularExpressions;

namespace poc.ai.ollama.generative.api.Helpers;

public static class StringHelpers
{
    public static string NormalizeSql(this string sql) =>
        sql.Trim()
           .TrimEnd(';')
           .Replace("\r", " ")
           .Replace("\n", " ");

    public static string SanitizeSql(this string sql)
    {
        var noComments = Regex.Replace(sql, @"--.*?$", "",
            RegexOptions.Multiline);

        return noComments.Trim().TrimEnd(';');
    }
}