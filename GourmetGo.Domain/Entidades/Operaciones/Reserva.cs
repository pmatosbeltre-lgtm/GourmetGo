using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

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

    // Validaciones
    public Reserva(DateTime fecha, TimeSpan hora, int cantidadPersonas, int usuarioId, int restauranteId)
    {
        if (usuarioId <= 0)
            throw new ReservaInvalidaExcepcion("Usuario inválido.");
        if (restauranteId <= 0)
            throw new ReservaInvalidaExcepcion("Restaurante inválido.");
        if (cantidadPersonas <= 0)
            throw new ReservaInvalidaExcepcion("Cantidad de personas inválida.");

        Fecha = fecha;
        Hora = hora;
        CantidadPersonas = cantidadPersonas;
        UsuarioId = usuarioId;
        RestauranteId = restauranteId;
        FechaCreacion = DateTime.UtcNow;
        Estado = EstadoReserva.Pendiente;
    }
}