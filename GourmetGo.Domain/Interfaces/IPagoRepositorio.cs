using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IPagoRepositorio
{
    Task<Pago?> ObtenerPorOrdenAsync(int ordenId);

    Task AgregarAsync(Pago pago);
}
