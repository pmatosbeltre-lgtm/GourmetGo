using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Social;
using GourmetGo.Application.Interfaces.Social;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Social;

public class ResenaService : IResenaService
{
    private readonly IResenaRepositorio _resenaRepositorio;

    public ResenaService(IResenaRepositorio resenaRepositorio)
    {
        _resenaRepositorio = resenaRepositorio;
    }

    public async Task<Result<ResenaDTO>> CrearResenaAsync(CreateResenaDTO dto)
    {
        if (dto is null)
            return Result<ResenaDTO>.Fail("La reseña no puede estar vacía.");

        if (dto.UsuarioId <= 0)
            return Result<ResenaDTO>.Fail("UsuarioId inválido.");

        if (dto.RestauranteId <= 0)
            return Result<ResenaDTO>.Fail("RestauranteId inválido.");

        if (dto.Calificacion < 1 || dto.Calificacion > 5)
            return Result<ResenaDTO>.Fail("La calificación debe estar entre 1 y 5.");

        if (string.IsNullOrWhiteSpace(dto.Comentario))
            return Result<ResenaDTO>.Fail("Comentario inválido.");

        var resena = new Reseña(dto.UsuarioId, dto.RestauranteId, dto.Calificacion, dto.Comentario);

        await _resenaRepositorio.AgregarAsync(resena);

        return Result<ResenaDTO>.Ok(MapToDTO(resena), "Reseña creada correctamente.");
    }

    public async Task<Result<List<ResenaDTO>>> ObtenerResenasPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            return Result<List<ResenaDTO>>.Fail("RestauranteId inválido.");

        var resenas = await _resenaRepositorio.ObtenerPorRestauranteAsync(restauranteId);

        var data = resenas.Select(MapToDTO).ToList();

        return Result<List<ResenaDTO>>.Ok(data);
    }

    private static ResenaDTO MapToDTO(Reseña resena)
    {
        return new ResenaDTO
        {
            Id = resena.Id,
            Calificacion = resena.Calificacion,
            Comentario = resena.Comentario,
            Fecha = resena.Fecha,
            UsuarioId = resena.UsuarioId,
            RestauranteId = resena.RestauranteId
        };
    }
}