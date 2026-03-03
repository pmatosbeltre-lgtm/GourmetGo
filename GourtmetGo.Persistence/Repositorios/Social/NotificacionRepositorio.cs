using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Social;

public class NotificacionRepositorio : INotificacionRepositorio
{
    private readonly GourmetGoContext _context;

    public NotificacionRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int usuarioId)
    {
        return await _context.Notificaciones
            .Where(n => n.UsuarioId == usuarioId)
            .OrderByDescending(n => n.FechaEnvio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AgregarAsync(Notificacion notificacion)
    {
        await _context.Notificaciones.AddAsync(notificacion);
        await _context.SaveChangesAsync();
    }
}
