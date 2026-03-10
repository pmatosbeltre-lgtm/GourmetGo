using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Seguridad
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public RolUsuario Rol { get; set; }

        public EstadoUsuario Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}