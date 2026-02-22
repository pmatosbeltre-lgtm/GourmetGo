using GourmetGo.Domain.Base;

namespace GourmetGo.Domain.Entidades.Catalogo
{
    public class Menu : BaseEntity
    {
        public string Nombre { get; private set; }

        public bool Activo { get; private set; }

        public int RestauranteId { get; private set; }

        public Restaurante Restaurante { get; private set; }


        public ICollection<Plato> Platos { get; private set; }

        public Menu()
        {
            Platos = new List<Plato>();
        }
    }
}
