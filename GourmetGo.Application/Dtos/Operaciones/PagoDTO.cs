using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class PagoDTO
{
    public int Id { get; set; }

    public decimal Monto { get; set; }

    public string MetodoPago { get; set; }

    public EstadoPago EstadoPago { get; set; }

    public DateTime Fecha { get; set; }

    public int OrdenId { get; set; }
}