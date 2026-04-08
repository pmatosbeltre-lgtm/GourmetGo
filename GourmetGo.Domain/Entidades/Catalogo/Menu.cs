using GourmetGo.Domain.Base;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades.Catalogo
{
    public class Menu : BaseEntity
    {
        public string Nombre { get; private set; }
        public bool Activo { get; private set; }
        public int RestauranteId { get; private set; }
        public Restaurante Restaurante { get; private set; }

        public void Actualizar(string nombre, bool activo)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionDominio("El nombre del menú es obligatorio.");

            // Cambio de estado controlado
            Nombre = nombre;
            Activo = activo;
        }
        public ICollection<Plato> Platos { get; private set; }

        //validaciones
        public Menu(string nombre, int restauranteId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionDominio("El nombre del menú es obligatorio.");
            if (restauranteId <= 0)
                throw new ExcepcionDominio("El ID del restaurante es obligatorio.");

            Nombre = nombre;
            RestauranteId = restauranteId;
            Activo = true;

            Platos = new List<Plato>();
        }
    }
}
