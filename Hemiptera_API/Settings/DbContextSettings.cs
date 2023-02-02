namespace Hemiptera_API.Settings;

public static class DbContextSettings
{
    private static string? _connectionString;

    /// <summary>
    /// ConnectionString property to hold the value of the connection string.
    /// The value for this property is set in Program.cs based on the environment.
    /// </summary>
    public static string ConnectionString
    {
        get => GetConnectionString();
        set => SetConnectionString(value);
    }

    /// <summary>
    /// Method to retrieve the database connection string
    /// </summary>
    /// <returns>The connection string value</returns>
    private static string GetConnectionString()
    {
        return _connectionString
            ?? throw new InvalidOperationException
            ($"The 'ConnectionString' property of class '{nameof(DbContextSettings)}' must be set before use.");
    }

    /// <summary>
    /// Method to set the database connection string.
    /// </summary>
    /// <param name="value">The value to set the connection string to</param>
    private static void SetConnectionString(string value)
    {
        _connectionString = value ?? throw new ArgumentNullException
              ($"The value for the 'ConnectionString' property in class '{nameof(DbContextSettings)}' cannot be null.");
    }
}