namespace gain_api.Controllers
{
    using core.Models;
    using core.Services.Base;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/responsable")]
    public class ResponsablesController(IBaseService<Responsable> responsableService) : ControllerBase
    {
        private readonly IBaseService<Responsable> _responsableService = responsableService;

    }
}
