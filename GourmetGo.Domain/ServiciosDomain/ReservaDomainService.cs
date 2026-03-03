using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.ServiciosDomain
{
    public class ReservaDomainService
    {
        public void ValidarReserva(Restaurante restaurante, int cantidadPersonas)
        {
            if (restaurante.Estado != EstadoRestaurante.Activo)
                throw new ExcepcionDominio("No se puede reservar en un restaurante cerrado.");

            if (cantidadPersonas > restaurante.Capacidad)
                throw new ExcepcionDominio("La cantidad de personas supera la capacidad del restaurante.");
        }
    }
}
