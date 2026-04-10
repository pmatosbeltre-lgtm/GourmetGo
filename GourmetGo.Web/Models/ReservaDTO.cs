namespace GourmetGo.Web.Models
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int CantidadPersonas { get; set; }
        public int UsuarioId { get; set; }
        public int RestauranteId { get; set; }
    }
}