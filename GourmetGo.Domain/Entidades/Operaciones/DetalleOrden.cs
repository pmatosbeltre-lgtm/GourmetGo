using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;

namespace GourmetGo.Domain.Entidades;

public class DetalleOrden : BaseEntity
{
    public int OrdenId { get; private set; }

    public Orden Orden { get; private set; }

    public int PlatoId { get; private set; }

    public Plato Plato { get; private set; }

    public int Cantidad { get; private set; }

    public decimal PrecioUnitario { get; private set; }
}