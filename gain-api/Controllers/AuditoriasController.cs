namespace gain_api.Controllers
{
    using AutoMapper;
    using core.Dto;
    using core.Models;
    using core.Services.Auditoria;
    using core.Services.Base;
    using Filters;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/auditoria")]
    [ApiController]
    public class AuditoriasController(IBaseService<Auditoria> baseService, IAuditoriaService _auditoriaService, IMapper _mapper) : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> Get()
        {
            var result = await baseService.GetAllAsync($"{nameof(Auditoria.Responsable)}, {nameof(Auditoria.Hallazgos)}");
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await baseService.GetByIdAsync(id, $"{nameof(Auditoria.Responsable)}, {nameof(Auditoria.Hallazgos)}");
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin, [FromQuery] Estado estado)
        {
            var result = await _auditoriaService.Search(fechaInicio, fechaFin, estado);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpGet("ReporteVista")]
        public async Task<IActionResult> GetResumenVista()
        {
            var data = await _auditoriaService.GetResumenViewAsync();
            return Ok(data);
        }
        
        [HttpPost("Add")]
        [ServiceFilter(typeof(ValidationFilter<AuditoriaDto>))]
        public async Task<IActionResult> Add(AuditoriaDto model)
        {
            var auditoria = _mapper.Map<Auditoria>(model);
            var result = await baseService.AddAsync(auditoria);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpPatch("Update")]
        [ServiceFilter(typeof(ValidationFilter<UpdateAuditoriaDto>))]
        public async Task<IActionResult> Update(UpdateAuditoriaDto model)
        {
            var auditoria = _mapper.Map<Auditoria>(model);
            var result = await _auditoriaService.UpdateAuditoria(auditoria);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpPatch("SetResponsable/{id:int}")]
        [ServiceFilter(typeof(ValidationFilter<SetResponsableAuditoriaDto>))]
        public async Task<IActionResult> SetResponsable(int id, SetResponsableAuditoriaDto model)
        {
            if (id != model.AuditoriaId)
            {
                return NotFound(ResponseDto<string>.Failure("ID de la ruta no coincide con el cuerpo."));
            }
            
            var result = await _auditoriaService.SetResponsable(model);

            return result.Success switch
            {
                false => BadRequest(result),
                true => Ok(result)
            };

        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await baseService.Delete(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
