using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Catalogo;

public class RestauranteRepositorio : IRestauranteRepositorio
{
    private readonly GourmetGoContext _context;

    public RestauranteRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurante>> ObtenerTodosAsync()
    {
        return await _context.Restaurante
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Restaurante?> ObtenerPorIdAsync(int id)
    {
        return await _context.Restaurante
            .Include(r => r.Menus)
                .ThenInclude(m => m.Platos)
            .Include(r => r.Reseñas)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Restaurante?> ObtenerPorUsuarioIdAsync(int usuarioId)
    {
        return await _context.Restaurante
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.UsuarioId == usuarioId);
    }

    public async Task AgregarAsync(Restaurante restaurante)
    {
        await _context.Restaurante.AddAsync(restaurante);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Restaurante restaurante)
    {
        _context.Restaurante.Update(restaurante);
        await _context.SaveChangesAsync();
    }
}