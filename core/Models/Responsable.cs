namespace core.Models
{
    public class Responsable : BaseEntity
    {
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Area { get; set; } = null!;
        public List<Auditoria>?  Auditorias { get; set; } 
    }
}
