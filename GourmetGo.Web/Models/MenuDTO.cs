using System.Text.Json.Serialization;

public class MenuDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int RestauranteId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)] 
    public bool Activo { get; set; }
}