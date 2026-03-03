using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades;

public class Orden : BaseEntity
{
    public DateTime Fecha { get; private set; }
    public decimal Total { get; private set; }
    public EstadoOrden Estado { get; private set; }
    public string TipoOrden { get; private set; }
    public DateTime FechaCreacion { get; private set; }
   
    public int UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; }
   
    public int RestauranteId { get; private set; }
    public Restaurante Restaurante { get; private set; }
   
    public Pago Pago { get; private set; }

    public ICollection<DetalleOrden> Detalles { get; private set; }

    // Validaciones
    public Orden(DateTime fecha, string tipoOrden, int usuarioId, int restauranteId)
    {
        if (usuarioId <= 0)
            throw new OrdenInvalidaExcepcion("Usuario inválido.");

        if (restauranteId <= 0)
            throw new OrdenInvalidaExcepcion("Restaurante inválido.");

        if (string.IsNullOrWhiteSpace(tipoOrden))
            throw new OrdenInvalidaExcepcion("Tipo de orden inválido.");

        Fecha = fecha;
        TipoOrden = tipoOrden;
        UsuarioId = usuarioId;
        RestauranteId = restauranteId;

        FechaCreacion = DateTime.UtcNow;
        Estado = EstadoOrden.Pendiente;
        Total = 0;

        Detalles = new List<DetalleOrden>();
    }
}