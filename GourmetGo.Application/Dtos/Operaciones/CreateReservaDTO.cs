namespace GourmetGo.Application.DTOs.Operaciones;

public class CreateReservaDTO
{
    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public int CantidadPersonas { get; set; }

    public int UsuarioId { get; set; }

    public int RestauranteId { get; set; }

    public string NombreCliente { get; set; } = string.Empty;
}