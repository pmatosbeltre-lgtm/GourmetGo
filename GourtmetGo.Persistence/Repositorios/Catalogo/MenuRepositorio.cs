using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Catalogo;

public class MenuRepositorio : IMenuRepositorio
{
    private readonly GourmetGoContext _context;

    public MenuRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Menu>> ObtenerPorRestauranteAsync(int restauranteId)
    {
        return await _context.Menus
            .Where(m => m.RestauranteId == restauranteId)
            .Include(m => m.Platos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Menu menu)
    {
        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Menu menu)
    {
        _context.Menus.Update(menu);
        await _context.SaveChangesAsync();
    }
}