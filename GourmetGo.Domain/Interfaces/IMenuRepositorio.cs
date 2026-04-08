using GourmetGo.Domain.Entidades.Catalogo;

namespace GourmetGo.Domain.Interfaces;

public interface IMenuRepositorio
{
    Task<IEnumerable<Menu>> ObtenerPorRestauranteAsync(int restauranteId);

    Task<Menu> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Menu menu);

    Task ActualizarAsync(Menu menu);

    Task EliminarAsync(int id);
}