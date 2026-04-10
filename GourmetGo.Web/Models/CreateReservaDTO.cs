using System.ComponentModel.DataAnnotations;

namespace GourmetGo.Web.Models
{
    public class CreateReservaDTO
    {
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateTime? Fecha { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "La hora es obligatoria.")]
        public TimeSpan? Hora { get; set; }

        [Required(ErrorMessage = "Indica para cuántas personas es la mesa.")]
        [Range(1, 20, ErrorMessage = "La reserva debe ser entre 1 y 20 personas.")]
        public int CantidadPersonas { get; set; } = 2;

        public int UsuarioId { get; set; }
        public int RestauranteId { get; set; }
    }
}
