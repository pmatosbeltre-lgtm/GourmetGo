using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.ServiciosDomain
{
    public class OrdenDomainService
    {
        public void ValidarOrdenAntesDeConfirmar(Orden orden)
        {
            if (orden.Detalles == null || !orden.Detalles.Any())
                throw new ExcepcionDominio("La orden debe tener al menos un detalle.");

            var totalCalculado = orden.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

            if (orden.Total != totalCalculado)
                throw new ExcepcionDominio("El total de la orden no coincide con los detalles.");
        }
    }
}
