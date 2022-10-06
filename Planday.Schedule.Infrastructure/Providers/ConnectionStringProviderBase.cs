using System.Text.RegularExpressions;

namespace Planday.Schedule.Infrastructure.Providers;

public static class ConnectionStringProviderBase
{
    public static string ProcessConnectionString(string connectionString)
    {
        const string pattern = "(.*=)(.*)(;.*)";
        var match = Regex.Match(connectionString, pattern);
        return Regex.Replace(
            connectionString,
            pattern,
            $"$1{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, match.Groups[2].Value)}$3");
    }
}