using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface IAuditoriaRepositorio
{
    Task AgregarAsync(AuditoriaEntity auditoria);
}