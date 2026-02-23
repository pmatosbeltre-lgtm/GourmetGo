using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Enums;

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

    public Orden()
    {
        Detalles = new List<DetalleOrden>();
    }
}