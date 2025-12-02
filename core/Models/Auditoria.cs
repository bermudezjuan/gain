namespace core.Models
{
    public class Auditoria : BaseEntity
    {
        public string Titulo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Area { get; set; } = null!;
        public Estado Estado { get; set; }
        public int? ResponsableId { get; set; }
        public Responsable? Responsable { get; set; }
        public List<Hallazgo>? Hallazgos { get; set; } 
    }
    
    public enum Estado {
        Pendiente = 1,
        EnProceso = 2,
        Finalizada = 3
    }
}
