using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Plato;

namespace GourmetGo.Application.Interfaces.Catalogo
{
    public interface IPlatoService
    {
        Task<Result<List<PlatoDTO>>> ObtenerPorMenuAsync(int menuId);

        Task<Result<string>> CrearAsync(CreatePlatoDTO dto);
    }
}