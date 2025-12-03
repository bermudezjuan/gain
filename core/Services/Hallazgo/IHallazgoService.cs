namespace core.Services.Hallazgo
{
    using Base;
    using Dto;
    using Models;

    public interface IHallazgoService : IBaseService<Hallazgo>
    {
        Task<ResponseDto<Hallazgo>> DeleteHallazgo(int id);
        Task<ResponseDto<List<Hallazgo>>> SearchHallazgos(int auditoriaId, Severidad severidad);
    }
}
