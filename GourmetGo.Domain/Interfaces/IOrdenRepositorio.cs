using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IOrdenRepositorio
{
    Task<Orden?> ObtenerPorIdAsync(int id);

    Task<IEnumerable<Orden>> ObtenerPorUsuarioAsync(int usuarioId);

    Task AgregarAsync(Orden orden);

    Task ActualizarAsync(Orden orden);
}