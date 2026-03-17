namespace GourmetGo.Application.DTOs.Social;

public class NotificacionDTO
{
    public int Id { get; set; }

    public string Tipo { get; set; } = string.Empty;

    public string Mensaje { get; set; } = string.Empty;

    public DateTime FechaEnvio { get; set; }

    public int UsuarioId { get; set; }
}