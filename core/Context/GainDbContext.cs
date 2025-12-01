namespace core.Context
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class GainDbContext : DbContext
    {
        public GainDbContext(DbContextOptions<GainDbContext> options)
            : base(options) {}
        
        public DbSet<Auditoria> Auditorias { get; set; }
        public DbSet<Hallazgo> Hallazgos { get; set; }
        public DbSet<Responsable> Responsables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property(nameof(BaseEntity.FechaCreacion))
                        .HasDefaultValueSql("GETUTCDATE()");
                }
            }
        }
    }
}
