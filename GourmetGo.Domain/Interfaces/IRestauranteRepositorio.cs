using GourmetGo.Domain.Entidades.Catalogo;

namespace GourmetGo.Domain.Interfaces;

public interface IRestauranteRepositorio
{
    Task<IEnumerable<Restaurante>> ObtenerTodosAsync();

    Task<Restaurante?> ObtenerPorIdAsync(int id);

    Task AgregarAsync(Restaurante restaurante);

    Task ActualizarAsync(Restaurante restaurante);
}