namespace core.Services.Responsable
{
    using Base;
    using Context;
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class ResponsableService (GainDbContext context) : BaseService<Responsable>(context), IResponsableService
    {
        private readonly GainDbContext _context = context;
        
        public async Task<ResponseDto<Responsable>> UpdateResponsable(Responsable entity)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity.Id);

                if (existingEntity == null )
                    return ResponseDto<Responsable>.Failure("No se puede actualizar la entidad, ");
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.FechaActualizacion = DateTime.UtcNow;
                _context.Entry(existingEntity).State = EntityState.Modified;
                var result = await SaveChangesAsync();
                
                return result > 0 
                    ? ResponseDto<Responsable>.Ok(existingEntity, "") 
                    : ResponseDto<Responsable>.Failure("No se pudo actualizar la entidad.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<Responsable>.Failure("No se pudo actualizar la entidad.");
            }
        }
    }
}
