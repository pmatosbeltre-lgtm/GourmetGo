using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;


namespace GourmetGo.Persistence.Repositories.Operaciones;

public class DetalleOrdenRepositorio : IDetalleOrdenRepositorio
{
    private readonly GourmetGoContext _context;

    public DetalleOrdenRepositorio(GourmetGoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AgregarAsync(DetalleOrden detalleOrden)
    {
        if (detalleOrden == null)
            throw new ArgumentNullException(nameof(detalleOrden));

        try
        {
            await _context.DetallesOrden.AddAsync(detalleOrden);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al guardar el detalle de la orden.", ex);
        }
    }

    public async Task<IEnumerable<DetalleOrden>> ObtenerPorOrdenAsync(int ordenId)
    {
        return await _context.DetallesOrden
            .AsNoTracking()
            .Where(d => d.OrdenId == ordenId)
            .ToListAsync();
    }
}