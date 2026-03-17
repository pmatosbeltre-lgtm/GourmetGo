namespace GourmetGo.Application.DTOs.Catalogo.Menu;

public class CreateMenuDTO
{
    public string Nombre { get; set; } = string.Empty;
    public int RestauranteId { get; set; }
}