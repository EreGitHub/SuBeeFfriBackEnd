using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository Repository;
        public ProductoController(IProductoRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await Repository.All());
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> All(string query)
        {
            return Ok(await Repository.Buscar(query));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductoDTO dto)
        {
            return Ok(await Repository.Add(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductoDTO dto)
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
