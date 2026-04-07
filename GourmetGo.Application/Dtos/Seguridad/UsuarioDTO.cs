using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Seguridad
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; }

        public EstadoUsuario Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}