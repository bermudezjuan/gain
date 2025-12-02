namespace core.Models
{
    public class Hallazgo : BaseEntity
    {
        public string Descripcion { get; set; } = null!;
        public Tipo Tipo { get; set; }
        public Severidad Severidad { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public int? AuditoriaId { get; set; }
        public Auditoria? Auditoria { get; set; }
    }

    public enum Severidad
    {
        Baja = 1, 
        Media = 2, 
        Alta = 3
    }
    
    public enum Tipo
    {
        Observacion = 1, 
        NoConformidad = 2,
    }
    
}
