using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Restaurante;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Catalogo;

public class RestauranteService : BaseService, IRestauranteService
{
    private readonly IRestauranteRepositorio _repositorio;

    public RestauranteService(IRestauranteRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Result<List<RestauranteDTO>>> ObtenerTodosAsync()
    {
        var restaurantes = await _repositorio.ObtenerTodosAsync();

        var data = restaurantes.Select(r => new RestauranteDTO
        {
            Id = r.Id,
            Nombre = r.Nombre,
            Direccion = r.Direccion,
            Capacidad = r.Capacidad
        }).ToList();

        return Result<List<RestauranteDTO>>.Ok(data);
    }

    public async Task<Result<RestauranteDTO>> ObtenerPorIdAsync(int id)
    {
        var restaurante = await _repositorio.ObtenerPorIdAsync(id);

        if (restaurante == null)
            return Result<RestauranteDTO>.Fail("Restaurante no encontrado");

        var dto = new RestauranteDTO
        {
            Id = restaurante.Id,
            Nombre = restaurante.Nombre,
            Direccion = restaurante.Direccion,
            Capacidad = restaurante.Capacidad
        };

        return Result<RestauranteDTO>.Ok(dto);
    }

    public async Task<Result<string>> CrearAsync(CreateRestauranteDTO dto)
    {
        var restaurante = new Restaurante(
            dto.Nombre,
            dto.Direccion,
            dto.Capacidad,
            Domain.Enums.EstadoRestaurante.Activo
        );

        await _repositorio.AgregarAsync(restaurante);

        return Result<string>.Ok("Restaurante creado correctamente");
    }
}