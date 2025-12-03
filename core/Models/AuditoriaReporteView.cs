namespace core.Models
{
    public class AuditoriaReporteView
    {
        public int AuditoriaId { get; set; } 
        public string Titulo { get; set; }
        public int HallazgosAlta { get; set; }
        public int HallazgosMedia { get; set; }
        public int HallazgosBaja { get; set; }
        public int HallazgosTotal { get; set; }
    }
}
