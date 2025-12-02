namespace core.Dto
{
    using Models;

    public class HallazgoDto
    {
        public string Descripcion { get; set; } = null!;
        public Tipo Tipo { get; set; }
        public Severidad Severidad { get; set; }
        public DateTime FechaDeteccion { get; set; } =  DateTime.Now;
    }
}
