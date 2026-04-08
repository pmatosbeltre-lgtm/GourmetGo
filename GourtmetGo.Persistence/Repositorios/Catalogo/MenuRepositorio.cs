using System;
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
        return await _context.Menu
            .Where(m => m.RestauranteId == restauranteId)
            .Include(m => m.Platos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Menu> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException(nameof(id), "El id debe ser mayor que cero.");

        var menu = await _context.Menu
            .Include(m => m.Platos)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menu is null)
            throw new InvalidOperationException($"Menu with id {id} was not found.");

        return menu;
    }

    public async Task AgregarAsync(Menu menu)
    {
        await _context.Menu.AddAsync(menu);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Menu menu)
    {
        _context.Menu.Update(menu);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var menu = await _context.Menu
            .Include(m => m.Platos)  // ← Trae platos para cascada
            .FirstOrDefaultAsync(m => m.Id == id);

        if (menu != null)
        {
            _context.Menu.Remove(menu);  // ← EF elimina cascada automáticamente
            await _context.SaveChangesAsync();
        }
    }
}