namespace gain_api.Controllers
{
    using AutoMapper;
    using core.Dto;
    using core.Models;
    using core.Services.Auditoria;
    using core.Services.Base;
    using Filters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/hallazgo")]
    public class HallazgosController(IBaseService<Hallazgo> baseService, IAuditoriaService _auditoriaService, IMapper _mapper) : ControllerBase
    {
        [HttpPost("Add")]
        [ServiceFilter(typeof(ValidationFilter<HallazgoDto>))]
        public async Task<IActionResult> Add(HallazgoDto model)
        {
            var hallazgo = _mapper.Map<Hallazgo>(model);
            var result = await baseService.AddAsync(hallazgo);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpPatch("SetAuditoria/{id:int}")]
        [ServiceFilter(typeof(ValidationFilter<SetHallazgoAuditoriaDto>))]
        public async Task<IActionResult> SetAuditoria(int id, SetHallazgoAuditoriaDto model)
        {
            if (id != model.AuditoriaId)
            {
                return NotFound(ResponseDto<string>.Failure("ID de la ruta no coincide con el cuerpo."));
            }
            
            var result = await _auditoriaService.SetHallazgo(model);

            return result.Success switch
            {
                false => BadRequest(result),
                true => Ok(result)
            };

        }
    }
}
