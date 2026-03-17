using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades;

public class Pago : BaseEntity
{
    public decimal Monto { get; private set; }
    public string MetodoPago { get; private set; }

    public EstadoPago EstadoPago { get; private set; }
    public DateTime Fecha { get; private set; }

    public int OrdenId { get; private set; }
    public Orden? Orden { get; private set; }

    // Validaciones
    public Pago(decimal monto, string metodoPago, int ordenId)
    {
        if (monto <= 0)
            throw new PagoInvalidoExcepcion("Monto inválido.");
        if (string.IsNullOrWhiteSpace(metodoPago))
            throw new PagoInvalidoExcepcion("Método de pago inválido.");
        if (ordenId <= 0)
            throw new PagoInvalidoExcepcion("Orden inválida.");

        Monto = monto;
        MetodoPago = metodoPago;
        OrdenId = ordenId;
        Fecha = DateTime.UtcNow;
        EstadoPago = EstadoPago.Pendiente;
    }
}