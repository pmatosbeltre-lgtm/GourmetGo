using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IOrdenRepositorio
{
    Task<Orden?> ObtenerPorIdAsync(int id);

    Task<IEnumerable<Orden>> ObtenerPorUsuarioAsync(int usuarioId);

    Task AgregarAsync(Orden orden);
    //Task<List<Orden>> ObtenerPorRestauranteAsync(int restauranteId);
    //Task<List<Orden>> ActualizarAsync(Orden orden);

    // Cambia Task<List<Orden>> por Task<IEnumerable<Orden>> o Task<List<Orden>> 
    // según lo que realmente devuelva tu base de datos.
    Task<IEnumerable<Orden>> ObtenerPorRestauranteAsync(int restauranteId);

    // Lo más probable es que ActualizarAsync deba devolver solo la tarea o la entidad, no una lista.
    Task ActualizarAsync(Orden orden);
}