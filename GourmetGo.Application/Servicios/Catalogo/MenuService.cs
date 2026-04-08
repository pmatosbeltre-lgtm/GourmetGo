using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Catalogo;

public class MenuService : BaseService, IMenuService
{
    private readonly IMenuRepositorio _repositorio;

    public MenuService(IMenuRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Result<List<MenuDTO>>> ObtenerPorRestauranteAsync(int restauranteId)
    {
        // Validación de seguridad
        if (restauranteId <= 0)
            return Result<List<MenuDTO>>.Fail("El ID del restaurante no es válido.");

        var menus = await _repositorio.ObtenerPorRestauranteAsync(restauranteId);

        var data = menus.Select(m => new MenuDTO
        {
            Id = m.Id,
            Nombre = m.Nombre,
            Activo = m.Activo,
            RestauranteId = m.RestauranteId
        }).ToList();

        return Result<List<MenuDTO>>.Ok(data);
    }

    public async Task<Result<string>> CrearAsync(CreateMenuDTO dto)
    {
        //validaciones
        if (dto == null)
            return Result<string>.Fail("La información del menú no puede estar vacía.");

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            return Result<string>.Fail("El nombre del menú es obligatorio.");

        if (dto.RestauranteId <= 0)
            return Result<string>.Fail("El restaurante asociado no es válido.");

        // Creación y persistencia
        var menu = new Menu(dto.Nombre, dto.RestauranteId);

        await _repositorio.AgregarAsync(menu);

        return Result<string>.Ok("Menú creado correctamente");
    }

    public async Task<Result<string>> ActualizarAsync(int id, UpdateMenuDTO dto)
    {
        if (id <= 0)
            return Result<string>.Fail("El ID del menú no es válido.");

        if (dto == null)
            return Result<string>.Fail("La información del menú no puede estar vacía.");

        if (string.IsNullOrWhiteSpace(dto.Nombre))
            return Result<string>.Fail("El nombre del menú es obligatorio.");

        var menu = await _repositorio.ObtenerPorIdAsync(id);

        if (menu == null)
            return Result<string>.Fail("Menú no encontrado.");

        await _repositorio.ActualizarAsync(menu);

        return Result<string>.Ok("Menú actualizado correctamente.");
    }

    public async Task<Result<string>> EliminarAsync(int id)
    {
        if (id <= 0)
            return Result<string>.Fail("El ID del menú no es válido.");

        var menu = await _repositorio.ObtenerPorIdAsync(id);

        if (menu == null)
            return Result<string>.Fail("Menú no encontrado.");

        await _repositorio.EliminarAsync(id);

        return Result<string>.Ok("Menú y sus platos eliminados correctamente.");
    }

    public async Task<Result<MenuDTO>> ObtenerPorIdAsync(int id)
    {
        if (id <= 0)
            return Result<MenuDTO>.Fail("El ID del menú no es válido.");

        var menu = await _repositorio.ObtenerPorIdAsync(id);

        if (menu == null)
            return Result<MenuDTO>.Fail("Menú no encontrado.");

        var dto = new MenuDTO
        {
            Id = menu.Id,
            Nombre = menu.Nombre,
            Activo = menu.Activo,
            RestauranteId = menu.RestauranteId
        };

        return Result<MenuDTO>.Ok(dto);
    }

}