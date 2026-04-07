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
        // Validación de seguridad
        if (menuId <= 0)
            return Result<List<PlatoDTO>>.Fail("El ID del menú no es válido.");

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
        // Validaciones 
        if (dto == null)
            return Result<string>.Fail("La información del plato no puede estar vacía.");

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            return Result<string>.Fail("El nombre del plato es obligatorio.");

        if (dto.Precio <= 0)
            return Result<string>.Fail("El precio del plato debe ser mayor a cero.");

        if (dto.MenuId <= 0)
            return Result<string>.Fail("El plato debe estar asociado a un menú válido.");

        // Creación y persistencia
        var plato = new Plato(dto.Nombre, dto.Precio, dto.MenuId);

        await _repositorio.AgregarAsync(plato);

        return Result<string>.Ok("Plato creado correctamente");
    }
}