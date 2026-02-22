using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IReservaRepositorio
{
    Task<Reserva?> ObtenerPorIdAsync(int id);

    Task<IEnumerable<Reserva>> ObtenerPorRestauranteAsync(int restauranteId);

    Task AgregarAsync(Reserva reserva);

    Task ActualizarAsync(Reserva reserva);
}