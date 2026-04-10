namespace GourmetGo.Web.Models
{
    public class PlatoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

       
        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public int MenuId { get; set; }

        public string PrecioFormateado => Precio.ToString("C2", new System.Globalization.CultureInfo("es-DO"));
    }
}