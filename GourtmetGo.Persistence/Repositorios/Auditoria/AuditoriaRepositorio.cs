using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Seguridad;

public class AuditoriaRepositorio : IAuditoriaRepositorio
{
    private readonly GourmetGoContext _context;

    public AuditoriaRepositorio(GourmetGoContext context)
    {
        _context = context;
    }

    public async Task AgregarAsync(AuditoriaEntity auditoria)
    {
        await _context.Auditoria.AddAsync(auditoria);
        await _context.SaveChangesAsync();
    }
}
