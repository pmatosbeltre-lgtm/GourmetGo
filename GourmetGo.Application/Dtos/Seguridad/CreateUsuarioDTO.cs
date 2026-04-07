 using GourmetGo.Domain.Enums;
namespace GourmetGo.Application.Dtos.Seguridad
{
    public class CreateUsuarioDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; } 
    }
}
