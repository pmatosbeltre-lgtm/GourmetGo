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
        // Validaciones 
        if (dto == null)
            return Result<string>.Fail("Los datos de auditoría no pueden estar vacíos.");

        if (string.IsNullOrWhiteSpace(dto.Accion))
            return Result<string>.Fail("La acción a auditar no puede estar vacía.");

        if (dto.UsuarioId <= 0)
            return Result<string>.Fail("El ID del usuario es inválido.");

        //crear entidad de auditoría
        var auditoria = new AuditoriaEntity(
            dto.Accion,
            dto.UsuarioId,
            GetCurrentDate()
        );

       
        await _repositorio.AgregarAsync(auditoria);

        return Result<string>.Ok("Auditoría registrada correctamente");
    }
}