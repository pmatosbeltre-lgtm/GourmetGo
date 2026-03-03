using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Social;

public class ReseñaRepositorio : IResenaRepositorio
{
    private readonly GourmetGoContext _context;

    public ReseñaRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reseña>> ObtenerPorRestauranteAsync(int restauranteId)
    {
        return await _context.Resenas
            .Where(r => r.RestauranteId == restauranteId)
            .Include(r => r.Usuario)
            .OrderByDescending(r => r.Fecha)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Reseña resena)
    {
        await _context.Resenas.AddAsync(resena);
        await _context.SaveChangesAsync();
    }
}