using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Auditoria;
using GourmetGo.Application.Interfaces.Auditoria;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;


namespace GourmetGo.Application.Services.Auditoria;

public class AuditoriaService : BaseService, IAuditoriaService
{
    private readonly IAuditoriaRepositorio _repositorio;

    public AuditoriaService(IAuditoriaRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Result<string>> RegistrarAsync(CreateAuditoriaDTO dto)
    {
        var auditoria = new AuditoriaEntity(
            dto.Accion,
            dto.UsuarioId,
            GetCurrentDate()
        );

        await _repositorio.AgregarAsync(auditoria);

        return Result<string>.Ok("Auditoría registrada correctamente");
    }
}