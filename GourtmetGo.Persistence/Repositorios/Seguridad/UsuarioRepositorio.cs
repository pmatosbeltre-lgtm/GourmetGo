using Microsoft.EntityFrameworkCore;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;

namespace GourmetGo.Persistence.Repositories.Seguridad;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly GourmetGoContext _context;

    public UsuarioRepositorio(GourmetGoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Usuario?> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id debe ser mayor que cero.");

        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> ObtenerPorCorreoAsync(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            throw new ArgumentException("El correo no puede estar vacío.");

        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Correo == correo);
    }

    public async Task AgregarAsync(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al guardar el usuario en la base de datos.", ex);
        }
    }

    public async Task ActualizarAsync(Usuario usuario)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        try
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Ocurrió un error al actualizar el usuario.", ex);
        }
    }
}