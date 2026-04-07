using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades.Catalogo;

public class Restaurante : BaseEntity
{
    public string Nombre { get; private set; } 
    public string Direccion { get; private set; }
    public int Capacidad { get; private set; }
    public EstadoRestaurante Estado { get; private set; }

    public int? UsuarioId { get; private set; }

    public void AsignarUsuario(int usuarioId)
    {
        if (usuarioId <= 0) throw new ExcepcionDominio("UsuarioId inválido.");
        UsuarioId = usuarioId;
    }

    //Relaciones

    public ICollection<Menu> Menus { get; private set; }
    public ICollection<Reserva> Reservas { get; private set; }
    public ICollection<Orden> Ordenes { get; private set; }
    public ICollection<Reseña> Reseñas { get; private set; }

    // Validaciones
    public Restaurante(string nombre, string direccion, int capacidad, EstadoRestaurante estado)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionDominio("El nombre es obligatorio.");
        if (capacidad <= 0)
            throw new ExcepcionDominio("La capacidad debe ser mayor que 0.");
        if (string.IsNullOrWhiteSpace(direccion))
            throw new ExcepcionDominio("La dirección es obligatoria.");

        Nombre = nombre;
        Direccion = direccion;
        Capacidad = capacidad;
        Estado = estado;

        Menus = new List<Menu>();
        Reservas = new List<Reserva>();
        Ordenes = new List<Orden>();
        Reseñas = new List<Reseña>();
    }
}