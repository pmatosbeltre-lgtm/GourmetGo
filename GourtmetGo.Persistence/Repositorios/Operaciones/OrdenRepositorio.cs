using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Operaciones;

public class OrdenRepositorio : IOrdenRepositorio
{
    private readonly GourmetGoContext _context;

    public OrdenRepositorio(GourmetGoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Orden?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id debe ser mayor que cero.");

        return await _context.Ordenes
            .Include(o => o.Usuario)
            .Include(o => o.Restaurante)
            .Include(o => o.Pago)
            .Include(o => o.Detalles)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Orden>> ObtenerPorUsuarioAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("El id del usuario debe ser válido.");

        return await _context.Ordenes
            .Where(o => o.UsuarioId == usuarioId)
            .Include(o => o.Restaurante)
            .Include(o => o.Pago)
            .Include(o => o.Detalles)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Orden orden)
    {
        if (orden == null)
            throw new ArgumentNullException(nameof(orden));

        try
        {
            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al guardar la orden.", ex);
        }
    }

    public async Task ActualizarAsync(Orden orden)
    {
        if (orden == null)
            throw new ArgumentNullException(nameof(orden));

        try
        {
            _context.Ordenes.Update(orden);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al actualizar la orden.", ex);
        }
    }
}