using GourmetGo.Application.Base;
using GourmetGo.Application.Dtos.Seguridad;
using GourmetGo.Application.DTOs.Seguridad;

namespace GourmetGo.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Result<UsuarioDTO>> CrearUsuario(CreateUsuarioDTO dto);

        Task<Result<UsuarioDTO>> ObtenerUsuario(int id);

        Task<Result<UsuarioDTO>> ObtenerUsuarioPorCorreo(string correo);
    }
}
