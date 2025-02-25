using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.ORM.Settings;

public static class EnvironmentVariables
{
    public static string GetDatabaseConnectionString()
    {
        var databaseConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

        if (string.IsNullOrEmpty(databaseConnectionString))
            databaseConnectionString = GetConfigurations().GetConnectionString("DefaultConnection");

        return databaseConnectionString ?? "";
    }

    private static IConfigurationRoot GetConfigurations()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}