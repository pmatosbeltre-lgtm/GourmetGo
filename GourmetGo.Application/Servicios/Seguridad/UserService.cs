using GourmetGo.Application.Base;
using GourmetGo.Application.Dtos.Seguridad;
using GourmetGo.Application.DTOs.Seguridad;
using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UserService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<Result<UserDTO>> CrearUsuario(CreateUserDTO dto)
        {
            try
            {
                var usuarioExistente = await _usuarioRepositorio.ObtenerPorCorreoAsync(dto.Correo);

                if (usuarioExistente != null)
                    return Result<UserDTO>.Fail("Ya existe un usuario con ese correo.");

                var usuario = new Usuario(
                    dto.Nombre,
                    dto.Correo,
                    dto.Contrasena,
                    dto.Rol
                );

                await _usuarioRepositorio.AgregarAsync(usuario);

                var userDto = new UserDTO
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Rol = usuario.Rol,
                    Estado = usuario.Estado,
                    FechaRegistro = usuario.FechaRegistro
                };

                return Result<UserDTO>.Ok(userDto, "Usuario creado correctamente");
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Fail($"Error al crear el usuario: {ex.Message}");
            }
        }

        public async Task<Result<UserDTO>> ObtenerUsuario(int id)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);

            if (usuario == null)
                return Result<UserDTO>.Fail("Usuario no encontrado");

            var dto = new UserDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };

            return Result<UserDTO>.Ok(dto);
        }

        public async Task<Result<UserDTO>> ObtenerUsuarioPorCorreo(string correo)
        {
            var usuario = await _usuarioRepositorio.ObtenerPorCorreoAsync(correo);

            if (usuario == null)
                return Result<UserDTO>.Fail("Usuario no encontrado");

            var dto = new UserDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };

            return Result<UserDTO>.Ok(dto);
        }
    }
}