using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Auditoria;

namespace GourmetGo.Application.Interfaces.Auditoria
{
    public interface IAuditoriaService
    {
        Task<Result<string>> RegistrarAsync(CreateAuditoriaDTO dto);
    }
}