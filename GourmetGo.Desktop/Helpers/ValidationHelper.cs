namespace GourmetGo.Desktop.Helpers;

public static class ValidationHelper
{
    public static bool IsNullOrWhiteSpace(string? value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsPositiveInt(int value)
        => value > 0;

    public static bool IsPositiveDecimal(decimal value)
        => value > 0;
}