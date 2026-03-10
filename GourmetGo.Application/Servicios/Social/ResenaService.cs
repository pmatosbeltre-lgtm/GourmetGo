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
        _resenaRepositorio = resenaRepositorio
            ?? throw new ArgumentNullException(nameof(resenaRepositorio));
    }

    public async Task<ResenaDTO> CrearResenaAsync(CreateResenaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.UsuarioId <= 0)
            throw new ArgumentException("Usuario inválido.");

        if (dto.RestauranteId <= 0)
            throw new ArgumentException("Restaurante inválido.");

        if (dto.Calificacion < 1 || dto.Calificacion > 5)
            throw new ArgumentException("La calificación debe estar entre 1 y 5.");

        if (string.IsNullOrWhiteSpace(dto.Comentario))
            throw new ArgumentException("Comentario inválido.");

        var resena = new Reseña(
            dto.UsuarioId,
            dto.RestauranteId,
            dto.Calificacion,
            dto.Comentario
        );

        await _resenaRepositorio.AgregarAsync(resena);

        return MapToDTO(resena);
    }

    public async Task<IEnumerable<ResenaDTO>> ObtenerResenasPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            throw new ArgumentException("Restaurante inválido.");

        var resenas = await _resenaRepositorio.ObtenerPorRestauranteAsync(restauranteId);

        return resenas.Select(MapToDTO);
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