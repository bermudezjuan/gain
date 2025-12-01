namespace core.Services.Base
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseService(DbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (includeProperties == null)
                return await query.ToListAsync();
            query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProp) => current.Include(includeProp.Trim()));

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (includeProperties == null)
                return (await _dbSet.FindAsync(id))!;
            query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProp) => current.Include(includeProp.Trim()));
            return (await query.FirstOrDefaultAsync(e => e.Id == id))!;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
