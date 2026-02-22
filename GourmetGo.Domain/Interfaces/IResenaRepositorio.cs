using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IResenaRepositorio
{
    Task<IEnumerable<Reseña>> ObtenerPorRestauranteAsync(int restauranteId);

    Task AgregarAsync(Reseña resena);
}