namespace GourmetGo.Desktop.Configuration;

public static class ApiConfiguration
{
    public const string BaseUrl = "http://localhost:5043";

    // Para evitar que la app se quede pegada si la API no responde.
    public static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30);
}