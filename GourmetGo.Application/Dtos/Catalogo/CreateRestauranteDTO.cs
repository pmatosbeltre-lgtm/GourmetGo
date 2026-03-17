namespace GourmetGo.Application.DTOs.Catalogo.Restaurante;

public class CreateRestauranteDTO
{
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public int Capacidad { get; set; }
}