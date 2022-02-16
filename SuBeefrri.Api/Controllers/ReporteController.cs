using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> All()
        {
            return Ok(await Repository.Reporte1());
        }
    }
}
