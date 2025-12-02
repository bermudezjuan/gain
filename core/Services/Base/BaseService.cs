namespace core.Services.Base
{
    using Context;
    using Dto;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BaseService<T>(GainDbContext context) : IBaseService<T> where T : BaseEntity
    {
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        #region READ

        public async Task<ResponseDto<List<T>>> GetAllAsync(string includeProperties = null!)
        {
            try
            {
                var query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includeProperties);
                var results = await query.ToListAsync();
                return ResponseDto<List<T>>.Ok(results, "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<List<T>>.Failure("Error al obtener todas las entidades: " + e.Message);
            }
        }

        public async Task<ResponseDto<T>> GetByIdAsync(int id, string? includeProperties = null)
        {
            try
            {
                var query = _dbSet.AsNoTracking();
                query = ApplyIncludes(query, includeProperties);
                var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
                return entity != null 
                    ? ResponseDto<T>.Ok(entity, null!) 
                    : ResponseDto<T>.Failure($"Entidad con ID {id} no encontrada.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<T>.Failure("Error al obtener la entidad por ID: " + e.Message);
            }
        }

  #endregion

        #region CREATE

        public async Task<ResponseDto<T>> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                var result = await SaveChangesAsync();
                return result > 0 
                    ? ResponseDto<T>.Ok(entity, null!) 
                    : ResponseDto<T>.Failure("No se guardaron los datos. Consulte al administrador.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<T>.Failure("Error al agregar la entidad: " + e.Message);
            }
        }

  #endregion

        #region UPDATE

        public async Task<ResponseDto<T>> Update(T entity)
        {
            try
            {
                var existingEntity = await _dbSet.FindAsync(entity.Id);
                if (existingEntity == null)
                {
                    return ResponseDto<T>.Failure("Entidad no encontrada");
                }
                
                context.Entry(existingEntity).CurrentValues.SetValues(entity);
                existingEntity.FechaActualizacion = DateTime.UtcNow;
                context.Entry(existingEntity).State = EntityState.Modified;
                var result = await SaveChangesAsync();
                
                return result > 0 
                    ? ResponseDto<T>.Ok(existingEntity, null!) 
                    : ResponseDto<T>.Failure("No se pudo actualizar la entidad.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<T>.Failure("Error al actualizar la entidad: " + e.Message);
            }
        }

  #endregion

        #region DELETE

        public async Task<ResponseDto<T>> Delete(int id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity == null) return ResponseDto<T>.Failure($"Entidad con ID {id} no encontrada.");
                _dbSet.Remove(entity);
                var result = await SaveChangesAsync();
                return result > 0 
                    ? ResponseDto<T>.Ok(entity, null!) 
                    : ResponseDto<T>.Failure("No se pudo eliminar la entidad.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return ResponseDto<T>.Failure("Error al eliminar la entidad: " + e.Message);
            
            }
        }

  #endregion

        public Task<int> SaveChangesAsync()
        {
            try
            {
                return context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private static IQueryable<T> ApplyIncludes(IQueryable<T> query, string? includeProperties)
        {
            if (string.IsNullOrWhiteSpace(includeProperties))
                return query;

            return includeProperties
                .Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProp) => current.Include(includeProp.Trim()));
        }
    }
}
