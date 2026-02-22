using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IAuditoriaRepositorio
{
    Task AgregarAsync(Auditoria auditoria);
}