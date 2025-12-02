namespace core.Services.Base
{
    using Dto;
    using Models;

    public interface IBaseService<T> where T : BaseEntity
    {
        Task<ResponseDto<List<T>>> GetAllAsync(string includeProperties = null!);
        Task<ResponseDto<T>> GetByIdAsync(int id, string includeProperties = null!);
        Task<ResponseDto<T>> AddAsync(T entity);
        Task<ResponseDto<T>> Update(T entity);
        Task<ResponseDto<T>> Delete(int id);
    }
}
