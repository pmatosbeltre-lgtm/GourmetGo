namespace GourmetGo.Desktop.Helpers;

public static class MessageHelper
{
    public static void Info(string message, string title = "GourmetGo")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void Error(string message, string title = "GourmetGo")
        => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

    public static bool Confirm(string message, string title = "Confirmación")
        => MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
}