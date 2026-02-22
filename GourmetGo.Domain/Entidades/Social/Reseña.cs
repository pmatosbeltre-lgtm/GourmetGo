using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;

namespace GourmetGo.Domain.Entidades;

public class Reseña : BaseEntity
{
    public int Calificacion { get; private set; }

    public string Comentario { get; private set; }

    public DateTime Fecha { get; private set; }

    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }

    public int RestauranteId { get; private set; }

    public Restaurante Restaurante { get; private set; }
}