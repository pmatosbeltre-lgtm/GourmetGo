namespace GourmetGo.Application.DTOs.Catalogo;

public class RestauranteDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public int Capacidad { get; set; }
}