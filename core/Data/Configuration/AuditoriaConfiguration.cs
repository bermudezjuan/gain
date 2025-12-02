namespace core.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
    {
        public void Configure(EntityTypeBuilder<Auditoria> builder)
        {
            builder.ToTable("Auditorias");
            builder.Property(p => p.Titulo).IsRequired();
            builder.Property(p => p.FechaInicio).IsRequired();
            builder.Property(p => p.FechaFin).IsRequired();
            builder.Property(p => p.Estado).IsRequired();
        
            builder.HasMany(c => c.Hallazgos)
                .WithOne(c => c.Auditoria)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        
        }
    }
}
