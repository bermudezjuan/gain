namespace gain_api.Controllers
{
    using core.Models;
    using core.Services.Base;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/auditoria")]
    public class AuditoriasController(IBaseService<Auditoria> auditoriaService) : ControllerBase
    {
        private readonly IBaseService<Auditoria> _auditoriaService = auditoriaService;
    }
}
