using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades;

public class DetalleOrden : BaseEntity
{
    public int OrdenId { get; private set; }
    public Orden Orden { get; private set; }

    public int PlatoId { get; private set; }
    public Plato Plato { get; private set; }

    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }

    // Validaciones
    public DetalleOrden(int ordenId, int platoId, int cantidad, decimal precioUnitario)
    {
        if (cantidad <= 0)
            throw new OrdenInvalidaExcepcion("Cantidad inválida");
        if (precioUnitario <= 0)
            throw new OrdenInvalidaExcepcion("Precio unitario inválido");
        if (ordenId <= 0)
            throw new OrdenInvalidaExcepcion("Orden inválida");
        if (platoId <= 0)
            throw new OrdenInvalidaExcepcion("Plato inválido");

        OrdenId = ordenId;
        PlatoId = platoId;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }
}