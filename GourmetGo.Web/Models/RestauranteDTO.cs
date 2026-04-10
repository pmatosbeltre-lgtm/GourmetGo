namespace GourmetGo.Web.Models
{
    public class RestauranteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string ImagenUrl { get; set; } = string.Empty;
        public int Capacidad { get; set; }
        public int UsuarioId { get; set; }
    }
}
