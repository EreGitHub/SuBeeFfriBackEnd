using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalRepository Repository;
        public SucursalController(ISucursalRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await Repository.All());
        }

        [HttpPost]
        public async Task<IActionResult> Add(SucursalDTO dto)
        {
            return Ok(await Repository.Add(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SucursalDTO dto)
        {
            await Repository.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Repository.Delete(id);
            return Ok();
        }
    }
}
