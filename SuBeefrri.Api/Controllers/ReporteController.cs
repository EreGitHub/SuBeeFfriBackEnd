using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository Repository;
        public ReporteController(IReporteRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Reporte1(DateTime fechaInicio, DateTime fechaFin)
        {
            return Ok(await Repository.Reporte1(fechaInicio, fechaFin));
        }

        [HttpGet]
        public async Task<IActionResult> Reporte2(DateTime fechaInicio, DateTime fechaFin)
        {
            return Ok(await Repository.Reporte2(fechaInicio, fechaFin));
        }
    }
}
