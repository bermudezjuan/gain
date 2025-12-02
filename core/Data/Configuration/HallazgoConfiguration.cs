namespace core.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class HallazgoConfiguration : IEntityTypeConfiguration<Hallazgo>
    {
        public void Configure(EntityTypeBuilder<Hallazgo> builder)
        {
            builder.ToTable("Hallazgos");
            builder.Property(p => p.Descripcion).IsRequired();
            builder.Property(p => p.Tipo).IsRequired();
            builder.Property(p => p.Severidad).IsRequired();
            builder.Property(p => p.FechaDeteccion).IsRequired();
        }
    }
}
