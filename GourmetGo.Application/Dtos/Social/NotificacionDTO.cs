namespace GourmetGo.Application.DTOs.Social;

public class NotificacionDTO
{
    public int Id { get; set; }

    public string Tipo { get; set; }

    public string Mensaje { get; set; }

    public DateTime FechaEnvio { get; set; }

    public int UsuarioId { get; set; }
}