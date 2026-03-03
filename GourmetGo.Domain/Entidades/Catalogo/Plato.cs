using GourmetGo.Domain.Base;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades.Catalogo
{
    public class Plato : BaseEntity
    {
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }
        public bool Disponible { get; private set; }
        public int MenuId { get; private set; }

        public Menu Menu { get; private set; }

        //validaciones
        public Plato(string nombre, decimal precio, int menuId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ExcepcionDominio("El nombre del plato es obligatorio.");
            if (precio <= 0)
                throw new ExcepcionDominio("El precio debe ser mayor que 0.");
            if (menuId <= 0)
                throw new ExcepcionDominio("El ID del menú es obligatorio.");

            Nombre = nombre;
            Precio = precio;
            MenuId = menuId;
            Disponible = true;
        }
    }
}
