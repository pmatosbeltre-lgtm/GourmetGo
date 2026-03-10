namespace GourmetGo.Application.DTOs.Social;

public class CreateResenaDTO
{
    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }

    public int Calificacion { get; set; }

    public string Comentario { get; set; }
}
