namespace GourmetGo.Application.DTOs.Social;

public class CreateNotificacionDTO
{
    public string Tipo { get; set; } = string.Empty;

    public string Mensaje { get; set; } = string.Empty;

    public int UsuarioId { get; set; }
}