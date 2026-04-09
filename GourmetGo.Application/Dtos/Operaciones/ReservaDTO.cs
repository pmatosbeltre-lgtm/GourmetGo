using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class ReservaDTO
{
    public int Id { get; set; }

    public string NombreCliente { get; set; } = string.Empty;

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public int CantidadPersonas { get; set; }

    public EstadoReserva Estado { get; set; }

    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }
}