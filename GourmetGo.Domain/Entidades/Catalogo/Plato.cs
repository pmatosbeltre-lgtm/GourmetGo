using GourmetGo.Domain.Base;

namespace GourmetGo.Domain.Entidades.Catalogo
{
    public class Plato : BaseEntity
    {
        public string Nombre { get; private set; }

        public decimal Precio { get; private set; }

        public bool Disponible { get; private set; }

        public int MenuId { get; private set; }

        public Menu Menu { get; private set; }
    }
}
