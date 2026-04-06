using GourmetGo.Application.Base;
using GourmetGo.Application.Dtos.Seguridad;
using GourmetGo.Application.DTOs.Seguridad;
using GourmetGo.Application.Interfaces;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioService(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<Result<UsuarioDTO>> CrearUsuario(CreateUsuarioDTO dto)
        {
            // Validaciones estructurales 
            if (dto == null)
                return Result<UsuarioDTO>.Fail("La información del usuario no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return Result<UsuarioDTO>.Fail("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Correo))
                return Result<UsuarioDTO>.Fail("El correo es obligatorio.");

            if (string.IsNullOrWhiteSpace(dto.Contrasena))
                return Result<UsuarioDTO>.Fail("La contraseña es obligatoria.");

            // Validación de negocio 
            var usuarioExistente = await _usuarioRepositorio.ObtenerPorCorreoAsync(dto.Correo);
            if (usuarioExistente != null)
                return Result<UsuarioDTO>.Fail("Ya existe un usuario registrado con ese correo.");

            // Creación y persistencia
            var usuario = new Usuario(
                dto.Nombre,
                dto.Correo,
                dto.Contrasena, 
                dto.Rol
            );

            await _usuarioRepositorio.AgregarAsync(usuario);

            // Mapeo y retorno
            var userDto = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };

            return Result<UsuarioDTO>.Ok(userDto, "Usuario creado correctamente");
        }

        public async Task<Result<UsuarioDTO>> ObtenerUsuario(int id)
        {
            if (id <= 0)
                return Result<UsuarioDTO>.Fail("El ID de usuario no es válido.");

            var usuario = await _usuarioRepositorio.ObtenerPorIdAsync(id);

            if (usuario == null)
                return Result<UsuarioDTO>.Fail("Usuario no encontrado");

            var dto = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };

            return Result<UsuarioDTO>.Ok(dto);
        }

        public async Task<Result<UsuarioDTO>> ObtenerUsuarioPorCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return Result<UsuarioDTO>.Fail("El correo no puede estar vacío.");

            var usuario = await _usuarioRepositorio.ObtenerPorCorreoAsync(correo);

            if (usuario == null)
                return Result<UsuarioDTO>.Fail("Usuario no encontrado");

            var dto = new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };

            return Result<UsuarioDTO>.Ok(dto);
        }
    }
}