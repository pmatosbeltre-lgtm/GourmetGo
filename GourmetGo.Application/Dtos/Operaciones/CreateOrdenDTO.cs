using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class CreateOrdenDTO
{
    public DateTime Fecha { get; set; }

    public string TipoOrden { get; set; }

    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }
    public List<CreateDetalleOrdenDTO> Detalles { get; set; } = new();
}