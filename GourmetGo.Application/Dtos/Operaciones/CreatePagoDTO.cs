using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class CreatePagoDTO
{
    public int OrdenId { get; set; }

    public decimal Monto { get; set; }

    public string MetodoPago { get; set; } = string.Empty;
}