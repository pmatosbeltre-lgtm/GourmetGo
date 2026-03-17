namespace GourmetGo.Application.DTOs.Social;

public class ResenaDTO
{
    public int Id { get; set; }

    public int Calificacion { get; set; }

    public string Comentario { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }
}