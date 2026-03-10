namespace GourmetGo.Application.DTOs.Social;

public class CreateNotificacionDTO
{
    public string Tipo { get; set; }

    public string Mensaje { get; set; }

    public int UsuarioId { get; set; }
}