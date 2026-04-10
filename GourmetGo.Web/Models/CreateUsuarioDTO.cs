using System.ComponentModel.DataAnnotations;


namespace GourmetGo.Web.Models
{
    public enum RolUsuario
    {
        Cliente = 1,
        Propietario = 2,
        Administrador = 3
    }

    public class CreateUsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Contrasena { get; set; } = string.Empty;


        [Required(ErrorMessage = "Debes confirmar tu contraseña.")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; } = RolUsuario.Cliente;
    }
}