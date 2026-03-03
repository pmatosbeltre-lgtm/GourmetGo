using GourmetGo.Domain.Base;
using GourmetGo.Domain.Entidades.Catalogo;
using GourmetGo.Domain.Excepciones;

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

    // Validaciones
    public Reseña(int usuarioId, int restauranteId, int calificacion, string comentario)
    {
        if (calificacion < 1 || calificacion > 5)
            throw new ExcepcionDominio("La calificación debe estar entre 1 y 5.");
        if (string.IsNullOrWhiteSpace(comentario))
            throw new ExcepcionDominio("El comentario es obligatorio.");
        if (usuarioId <= 0)
            throw new ExcepcionDominio("El ID del usuario es obligatorio.");
        if (restauranteId <= 0)
            throw new ExcepcionDominio("El ID del restaurante es obligatorio.");
      
        Calificacion = calificacion;
        Comentario = comentario;
        UsuarioId = usuarioId;
        RestauranteId = restauranteId;
        Fecha = DateTime.UtcNow;
    }
}