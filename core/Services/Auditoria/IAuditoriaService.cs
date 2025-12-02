namespace core.Services.Auditoria
{
    using Base;
    using Dto;
    using Models;

    public interface IAuditoriaService : IBaseService<Auditoria>
    {
        Task<ResponseDto<Auditoria>> UpdateAuditoria(Auditoria entity);
        Task<ResponseDto<List<Auditoria>>> Search(DateTime? fechaInicio, DateTime? fechaFin, Estado? estado);
        Task<ResponseDto<Auditoria>> SetResponsable(SetResponsableAuditoriaDto model);
        Task<ResponseDto<List<Auditoria>>> GetAuditoriasByResponsable(int id);
        Task<ResponseDto<Auditoria>> SetHallazgo(SetHallazgoAuditoriaDto model);
    }
}
