namespace core.Services.Responsable
{
    using Base;
    using Dto;
    using Models;

    public interface IResponsableService : IBaseService<Responsable>
    {
        Task<ResponseDto<Responsable>> UpdateResponsable(Responsable entity);
    }
}
