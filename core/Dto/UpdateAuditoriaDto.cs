namespace core.Dto
{
    using Models;

    public class UpdateAuditoriaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Area { get; set; } = null!;
        public Estado Estado { get; set; }
    }
}
