using GourmetGo.Domain.Entidades.Catalogo;

namespace GourmetGo.Domain.Interfaces;

public interface IPlatoRepositorio
{
    Task<IEnumerable<Plato>> ObtenerPorMenuAsync(int menuId);

    Task AgregarAsync(Plato plato);

    Task ActualizarAsync(Plato plato);
}