namespace GourmetGo.Web.Models
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string EstadoPago { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int OrdenId { get; set; }

      
        public string MontoFormateado => Monto.ToString("C2", new System.Globalization.CultureInfo("es-DO"));
    }

    public class CreatePagoDTO
    {
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = "Tarjeta";
        public int OrdenId { get; set; }
    }
}
