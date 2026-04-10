using System.Text.Json.Serialization;

public class CreatePlatoDTO
{
    public string Nombre { get; set; } = string.Empty;

    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Descripcion { get; set; } = null;

    public decimal Precio { get; set; }
    public int MenuId { get; set; }
}