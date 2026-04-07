namespace GourmetGo.Desktop.Helpers;

public static class TokenStore
{
    public static string? AccessToken { get; private set; }

    public static void SetToken(string token) => AccessToken = token;

    public static void Clear() => AccessToken = null;

    public static bool HasToken => !string.IsNullOrWhiteSpace(AccessToken);
}