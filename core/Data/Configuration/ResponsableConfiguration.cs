namespace core.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ResponsableConfiguration: IEntityTypeConfiguration<Responsable>
    {
        public void Configure(EntityTypeBuilder<Responsable> builder)
        {
            builder.ToTable("Responsables");
            builder.Property(p => p.Nombre).IsRequired();
            builder.Property(p => p.Correo).IsRequired();
            builder.Property(p => p.Area).IsRequired();
        
            builder.HasMany(c => c.Auditorias)
                .WithOne(c => c.Responsable)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
