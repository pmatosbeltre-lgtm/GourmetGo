using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Catalogo;

public class PlatoRepositorio : IPlatoRepositorio
{
    private readonly GourmetGoContext _context;

    public PlatoRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Plato>> ObtenerPorMenuAsync(int menuId)
    {
        return await _context.Platos
            .Where(p => p.MenuId == menuId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Plato plato)
    {
        await _context.Platos.AddAsync(plato);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(Plato plato)
    {
        _context.Platos.Update(plato);
        await _context.SaveChangesAsync();
    }
}