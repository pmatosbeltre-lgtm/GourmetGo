using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IDetalleOrdenRepositorio
{
    Task AgregarAsync(DetalleOrden detalleOrden);
}