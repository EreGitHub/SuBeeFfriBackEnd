using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository Repository;
        private readonly IWebHostEnvironment HostEnvironment;
        public ProductoController(IProductoRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            HostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await Repository.All());
        }

        [HttpGet("{query}")]
        public async Task<IActionResult> BuscarProducto(string query)
        {
            return Ok(await Repository.Buscar(query));
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductoDTO dto)
        {
            return Ok(await Repository.Add(dto));
        }

        [HttpPost]
        public async Task<IActionResult> SubirFoto([FromForm] ProductoFotoDTO dto)
        {
            var direccionFoto = await SubirFoto();
            await Repository.GuardarFoto(dto.IdProducto, direccionFoto);
            return Ok();
        }
        private async Task<string> SubirFoto()
        {
            var archivo = HttpContext.Request.Form.Files[0];
            string miRuta = HostEnvironment.WebRootPath;
            string uploads = Path.Combine(miRuta, "archivos");
            var NombreArchivo = Guid.NewGuid().ToString();
            var Extencion = Path.GetExtension(archivo.FileName);
            using (var fileStreams = new FileStream(Path.Combine(uploads, NombreArchivo + Extencion), FileMode.Create))
            {
                await archivo.CopyToAsync(fileStreams);
            }
            var location = new Uri($"{Request.Scheme}://{Request.Host}{Path.Combine("/archivos", NombreArchivo + Extencion)}");
            return location.AbsoluteUri;
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
