using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Operaciones;

public class PagoRepositorio : IPagoRepositorio
{
    private readonly GourmetGoContext _context;

    public PagoRepositorio(GourmetGoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Pago?> ObtenerPorOrdenAsync(int ordenId)
    {
        if (ordenId <= 0)
            throw new ArgumentException("El id de la orden debe ser válido.");

        return await _context.Pagos
            .Include(p => p.Orden)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.OrdenId == ordenId);
    }

    public async Task AgregarAsync(Pago pago)
    {
        if (pago == null)
            throw new ArgumentNullException(nameof(pago));

        try
        {
            await _context.Pagos.AddAsync(pago);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al registrar el pago.", ex);
        }
    }
}