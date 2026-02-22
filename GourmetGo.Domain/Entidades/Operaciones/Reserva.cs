using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Enums;

namespace GourmetGo.Domain.Entidades;

public class Reserva : BaseEntity
{
    public DateTime Fecha { get; private set; }

    public TimeSpan Hora { get; private set; }

    public int CantidadPersonas { get; private set; }

    public EstadoReserva Estado { get; private set; }

    public DateTime FechaCreacion { get; private set; }

    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }

    public int RestauranteId { get; private set; }

    public Restaurante Restaurante { get; private set; }
}