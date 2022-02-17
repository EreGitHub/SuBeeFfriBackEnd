using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteRepository Repository;
        public ReporteController(IReporteRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Reporte1()
        {
            return Ok(await Repository.Reporte1());
        }

        [HttpGet("{numeroMes}")]
        public async Task<IActionResult> Reporte2(int numeroMes)
        {
            return Ok(await Repository.Reporte2(numeroMes));
        }
    }
}
