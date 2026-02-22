using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IUsuarioRepositorio
{
    Task<Usuario?> ObtenerPorIdAsync(int id);

    Task<Usuario?> ObtenerPorCorreoAsync(string correo);

    Task AgregarAsync(Usuario usuario);

    Task ActualizarAsync(Usuario usuario);
}