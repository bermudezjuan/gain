namespace core.Models
{
    public class BaseEntity
    {
        public int Id {  get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaActualizacion {  get; set; }
    }
}
