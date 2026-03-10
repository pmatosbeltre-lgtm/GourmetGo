using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Plato;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Catalogo;

public class PlatoService : BaseService, IPlatoService
{
    private readonly IPlatoRepositorio _repositorio;

    public PlatoService(IPlatoRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Result<List<PlatoDTO>>> ObtenerPorMenuAsync(int menuId)
    {
        var platos = await _repositorio.ObtenerPorMenuAsync(menuId);

        var data = platos.Select(p => new PlatoDTO
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Precio = p.Precio,
            Disponible = p.Disponible,
            MenuId = p.MenuId
        }).ToList();

        return Result<List<PlatoDTO>>.Ok(data);
    }

    public async Task<Result<string>> CrearAsync(CreatePlatoDTO dto)
    {
        var plato = new Plato(dto.Nombre, dto.Precio, dto.MenuId);

        await _repositorio.AgregarAsync(plato);

        return Result<string>.Ok("Plato creado correctamente");
    }
}