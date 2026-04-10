using System;

namespace GourmetGo.Web.Models
{

    public enum EstadoUsuario
    {
        Activo = 1,
        Inactivo = 2,
    }

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
