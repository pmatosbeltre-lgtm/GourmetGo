namespace GourmetGo.Domain;

public class Pago
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public string MetodoPago { get; set; } = string.Empty;
    public int PedidoId { get; set; }
}

