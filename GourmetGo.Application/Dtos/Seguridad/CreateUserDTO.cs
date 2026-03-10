 using GourmetGo.Domain.Enums;
namespace GourmetGo.Application.Dtos.Seguridad
{
    public class CreateUserDTO
    {
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public RolUsuario Rol { get; set; }
    }
}
