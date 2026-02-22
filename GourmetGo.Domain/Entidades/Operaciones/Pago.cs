using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;

namespace GourmetGo.Domain.Entidades;

public class Pago : BaseEntity
{
    public decimal Monto { get; private set; }

    public string MetodoPago { get; private set; }

    public EstadoPago EstadoPago { get; private set; }

    public DateTime Fecha { get; private set; }

    public int OrdenId { get; private set; }

    public Orden Orden { get; private set; }
}