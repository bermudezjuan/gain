namespace gain_api.Mappers
{
    using AutoMapper;
    using core.Dto;
    using core.Models;

    public class AutoMapperProfile :  Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Auditoria, AuditoriaDto>().ReverseMap();
            CreateMap<Auditoria, UpdateAuditoriaDto>().ReverseMap();
            CreateMap<Responsable, ResponsableDto>().ReverseMap();
            CreateMap<Hallazgo, HallazgoDto>().ReverseMap();
        }
    }
}
