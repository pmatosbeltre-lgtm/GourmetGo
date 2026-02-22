namespace GourmetGo.Domain.Base
{
    public abstract class AuditEntity: BaseEntity
    {
        public DateTime FechaCreacion { get; protected set; }

        public DateTime? FechaModificacion { get; protected set; }

        protected AuditEntity ()
        {
            FechaCreacion = DateTime.UtcNow;
        }

        public void MarcarComoModificada()
        {
            FechaModificacion = DateTime.UtcNow;
        }
    }
}