namespace core.Services.Hallazgo
{
    using Base;
    using Context;
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HallazgoService(GainDbContext context) : BaseService<Hallazgo>(context), IHallazgoService
    {
        public async Task<ResponseDto<Hallazgo>> DeleteHallazgo(int id)
        {
            try
            {
                var hallazgoExistente = await _dbSet
                    .Include(h => h.Auditoria)
                    .FirstOrDefaultAsync(h => h.Id == id);

                if (hallazgoExistente?.Auditoria != null && hallazgoExistente.Auditoria.Estado != Estado.EnProceso)
                {
                    return ResponseDto<Hallazgo>.Failure($"Auditoría con ID {hallazgoExistente.Auditoria.Id} no esta en estado de Proceso");
                }
            
                if (hallazgoExistente == null)
                    return ResponseDto<Hallazgo>.Failure("No se pudo eliminar el hallazgo.");
            
                _dbSet.Remove(hallazgoExistente);
                var result = await SaveChangesAsync();
                return result > 0 
                    ? ResponseDto<Hallazgo>.Ok(hallazgoExistente, "Hallazgo eliminado con éxito.")
                    : ResponseDto<Hallazgo>.Failure("No se pudo eliminar el hallazgo.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<Hallazgo>.Failure("No se pudo eliminar el hallazgo.");
            }
        }

        public async Task<ResponseDto<List<Hallazgo>>> SearchHallazgos(int auditoriaId, Severidad severidad)
        {
            try
            {
                var query = _dbSet.AsNoTracking();
                query = query.Where(h => h.Auditoria != null && h.Auditoria.Id == auditoriaId && h.Severidad == severidad);
                return ResponseDto<List<Hallazgo>>.Ok(await query.ToListAsync(), "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<List<Hallazgo>>.Failure("No se pudo realizar la busqueda.");
            }
        }
    }
}
