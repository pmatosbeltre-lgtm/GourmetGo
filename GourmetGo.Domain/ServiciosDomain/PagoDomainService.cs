using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.ServiciosDomain
{
    public class PagoDomainService
    {
        public void ValidarPago(Pago pago, decimal monto, Orden orden)
        {
            if (pago.EstadoPago == EstadoPago.Pagado)
                throw new ExcepcionDominio("La orden ya fue pagada.");

            if (monto != orden.Total)
                throw new ExcepcionDominio("El monto del pago no coincide con el total de la orden.");
        }
    }
}
