namespace core.Dto
{
    public class UpdateResponsableDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Area { get; set; } = null!;
    }
}
