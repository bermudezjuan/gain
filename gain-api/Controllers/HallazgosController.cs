namespace gain_api.Controllers
{
    using core.Models;
    using core.Services.Base;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/hallazgo")]
    public class HallazgosController(IBaseService<Hallazgo> hallazgoService)
    {
        private readonly IBaseService<Hallazgo> _hallazgoService =  hallazgoService;
    }
}
