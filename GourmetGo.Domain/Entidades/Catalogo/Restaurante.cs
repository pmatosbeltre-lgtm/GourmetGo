using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;

namespace GourmetGo.Domain.Entidades.Catalogo;

public class Restaurante : BaseEntity
{
    public string Nombre { get; private set; } 

    public string Direccion { get; private set; }

    public int Capacidad { get; private set; }

    public EstadoRestaurante Estado { get; private set; }

    //Relaciones

    public ICollection<Menu> Menus { get; private set; }

    public ICollection<Reserva> Reservas { get; private set; }

    public ICollection<Orden> Ordenes { get; private set; }

    public ICollection<Reseña> Reseñas { get; private set; }

    public Restaurante()
    {
        Menus = new List<Menu>();
        Reservas = new List<Reserva>();
        Ordenes = new List<Orden>();
        Reseñas = new List<Reseña>();
    }
}