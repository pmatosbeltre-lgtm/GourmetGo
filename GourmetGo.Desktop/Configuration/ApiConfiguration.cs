namespace GourmetGo.Desktop.Configuration;

public static class ApiConfiguration
{
    public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
    public static string BaseUrl { get; set; } = "http://localhost:5043";
}
