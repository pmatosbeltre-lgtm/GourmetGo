namespace GourmetGo.Application.DTOs.Operaciones;

public class DetalleOrdenDTO
{
    public int Id { get; set; }
    public int OrdenId { get; set; }
    public int PlatoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}