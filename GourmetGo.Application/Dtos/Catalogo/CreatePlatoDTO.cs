namespace GourmetGo.Application.DTOs.Catalogo.Plato;

public class CreatePlatoDTO
{
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int MenuId { get; set; }
}