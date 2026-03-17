namespace GourmetGo.Application.DTOs.Seguridad;

public class LoginRequestDTO
{
    public string Correo { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}