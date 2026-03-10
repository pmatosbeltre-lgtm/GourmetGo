using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class OrdenDTO
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public decimal Total { get; set; }

    public EstadoOrden Estado { get; set; }

    public string TipoOrden { get; set; }

    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }
}