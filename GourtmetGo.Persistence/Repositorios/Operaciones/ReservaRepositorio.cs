using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Operaciones;

public class ReservaRepositorio : IReservaRepositorio
{
    private readonly GourmetGoContext _context;

    public ReservaRepositorio(GourmetGoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Reserva?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id debe ser mayor que cero.");

        return await _context.Reservas
            .Include(r => r.Usuario)
            .Include(r => r.Restaurante)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reserva>> ObtenerPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            throw new ArgumentException("El id del restaurante debe ser válido.");

        return await _context.Reservas
            .Where(r => r.RestauranteId == restauranteId)
            .Include(r => r.Usuario)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        try
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al guardar la reserva.", ex);
        }
    }

    public async Task ActualizarAsync(Reserva reserva)
    {
        if (reserva == null)
            throw new ArgumentNullException(nameof(reserva));

        try
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al actualizar la reserva.", ex);
        }
    }
    public async Task<IEnumerable<Reserva>> ObtenerPorUsuarioAsync(int usuarioId)
    {
        return await _context.Reservas
            .Where(r => r.UsuarioId == usuarioId)
            .ToListAsync();
    }
}