namespace GourmetGo.Application.DTOs.Catalogo;

public class PlatoDTO
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public decimal Precio { get; set; }

    public bool Disponible { get; set; }

    public int MenuId { get; set; }
}