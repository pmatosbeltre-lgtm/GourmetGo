namespace GourmetGo.Application.DTOs.Catalogo;

public class MenuDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public bool Activo { get; set; }

    public int RestauranteId { get; set; }
}