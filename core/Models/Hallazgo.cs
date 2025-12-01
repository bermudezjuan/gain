namespace core.Models
{
    public class Hallazgo : BaseEntity
    {
        public string Descripcion { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public Severidad Severidad { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public int? AuditoriaId { get; set; }
        public Auditoria? Auditoria { get; set; }
    }

    public enum Severidad
    {
        Baja, 
        Media, 
        Alta
    }
}
