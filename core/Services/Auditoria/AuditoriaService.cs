namespace core.Services.Auditoria
{
    using Base;
    using Context;
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AuditoriaService(GainDbContext context) : BaseService<Auditoria>(context), IAuditoriaService
    {
        private readonly GainDbContext _context = context;
        
        public async Task<ResponseDto<Auditoria>> UpdateAuditoria(Auditoria entity)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity.Id);

                if (existingEntity is not { Estado: Estado.Pendiente })
                    return ResponseDto<Auditoria>.Failure("No se puede actualizar la entidad, ");
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.FechaActualizacion = DateTime.UtcNow;
                _context.Entry(existingEntity).State = EntityState.Modified;
                var result = await SaveChangesAsync();
                
                return result > 0 
                    ? ResponseDto<Auditoria>.Ok(existingEntity, "") 
                    : ResponseDto<Auditoria>.Failure("No se pudo actualizar la entidad.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<Auditoria>.Failure("No se pudo actualizar la entidad.");
            }
        }
        
        public async Task<ResponseDto<List<Auditoria>>> Search(DateTime? fechaInicio, DateTime? fechaFin, Estado? estado)
        {
            try
            {
                var query = _dbSet.AsNoTracking();

                if (fechaInicio.HasValue)
                {
                    query = query.Where(a => a.FechaCreacion.Date >= fechaInicio.Value.Date);
                }

                if (fechaFin.HasValue)
                {
                    
                    query = query.Where(a => a.FechaFin.Date <= fechaFin.Value.Date); 
                }

                if (estado.HasValue)
                {
                    query = query.Where(a => a.Estado == estado.Value);
                }
                
                return ResponseDto<List<Auditoria>>.Ok(await query.ToListAsync(),"");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<List<Auditoria>>.Failure("No se pudo realizar la busqueda.");
            }
        }
        
        public async Task<ResponseDto<Auditoria>> SetResponsable(SetResponsableAuditoriaDto model)
        {
            try
            {
                var auditoriaExistente = await _dbSet
                    .Include(a => a.Responsable)
                    .FirstOrDefaultAsync(a => a.Id == model.AuditoriaId);

                if (auditoriaExistente == null)
                {
                    return ResponseDto<Auditoria>.Failure($"Auditoría con ID {model.AuditoriaId} no encontrada.");
                }
            
                auditoriaExistente.ResponsableId = model.ResponsableId;
                var result = await SaveChangesAsync();
                return result > 0 
                    ? ResponseDto<Auditoria>.Ok(auditoriaExistente, "Responsable asignado con éxito.")
                    : ResponseDto<Auditoria>.Failure("No se pudo asignar el responsable. Verifique el ID del responsable.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<Auditoria>.Failure("No se pudo asignar el responsable. Verifique el ID del responsable.");
            }
        }
        
        public async Task<ResponseDto<List<Auditoria>>>  GetAuditoriasByResponsable(int id)
        {
            try
            {
                var query = _dbSet.AsNoTracking();
                query = query.Where(a => a.ResponsableId == id);
                return ResponseDto<List<Auditoria>>.Ok(await query.ToListAsync(), "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<List<Auditoria>>.Failure("No se pudo realizar la consulta");
            }
        }
        
        public async Task<ResponseDto<Auditoria>> SetHallazgo(SetHallazgoAuditoriaDto model)
        {
            try
            {
                var auditoriaExistente = await _dbSet
                    .Include(a => a.Hallazgos)
                    .FirstOrDefaultAsync(a => a.Id == model.AuditoriaId);

                if (auditoriaExistente == null)
                {
                    return ResponseDto<Auditoria>.Failure($"Auditoría con ID {model.AuditoriaId} no encontrada.");
                }
            
                var hallazgo = await _context.Hallazgos.FindAsync(model.HallazgoId);
                if (hallazgo == null)
                {
                    return ResponseDto<Auditoria>.Failure($"Hallazgo con ID {model.HallazgoId} no encontrado.");
                }
                auditoriaExistente.Hallazgos ??= [];
                auditoriaExistente.Hallazgos.Add(hallazgo);
                var result = await SaveChangesAsync();
                return result > 0 
                    ? ResponseDto<Auditoria>.Ok(auditoriaExistente, "Hallazgo asignado a la auditoría con éxito.")
                    : ResponseDto<Auditoria>.Failure("No se pudo asignar el hallazgo a la auditoría.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<Auditoria>.Failure("No se pudo asignar el hallazgo a la auditoría.");
            }
        }
        
        public async Task<IEnumerable<AuditoriaReporteView>> GetResumenViewAsync()
        {
            return await _context.AuditoriaReporteViews 
                .AsNoTracking() 
                .ToListAsync();
        }
    }
}
