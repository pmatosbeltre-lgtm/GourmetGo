using GourmetGo.Application.Base;
using GourmetGo.Application.Dtos.Seguridad;
using GourmetGo.Application.DTOs.Seguridad;

namespace GourmetGo.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDTO>> CrearUsuario(CreateUserDTO dto);

        Task<Result<UserDTO>> ObtenerUsuario(int id);

        Task<Result<UserDTO>> ObtenerUsuarioPorCorreo(string correo);
    }
}
