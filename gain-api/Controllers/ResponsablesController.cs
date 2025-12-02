namespace gain_api.Controllers
{
    using AutoMapper;
    using core.Dto;
    using core.Models;
    using core.Services.Auditoria;
    using core.Services.Base;
    using core.Services.Responsable;
    using Filters;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/responsable")]
    public class ResponsablesController(IBaseService<Responsable> baseService, IResponsableService _responsableService, IAuditoriaService _auditoriaService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> Get()
        {
            var result = await baseService.GetAllAsync($"{nameof(Responsable.Auditorias)}, {nameof(Responsable.Auditorias)}.{nameof(Auditoria.Hallazgos)}");
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await baseService.GetByIdAsync(id, $"{nameof(Responsable.Auditorias)}, {nameof(Responsable.Auditorias)}.{nameof(Auditoria.Hallazgos)}");
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpGet("auditorias/{id:int}")]
        public async Task<IActionResult> GetAuditoriasByResponsable(int id)
        {
            var result = await _auditoriaService.GetAuditoriasByResponsable(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpPost("Add")]
        [ServiceFilter(typeof(ValidationFilter<ResponsableDto>))]
        public async Task<IActionResult> Add(ResponsableDto model)
        {
            var responsable = _mapper.Map<Responsable>(model);
            var result = await baseService.AddAsync(responsable);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        
        [HttpPatch("Update")]
        [ServiceFilter(typeof(ValidationFilter<UpdateResponsableDto>))]
        public async Task<IActionResult> Update(UpdateResponsableDto model)
        {
            var responsable = _mapper.Map<Responsable>(model);
            var result = await _responsableService.UpdateResponsable(responsable);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
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
