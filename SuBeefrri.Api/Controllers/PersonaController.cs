using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository Repository;
        private readonly IWebHostEnvironment HostEnvironment;

        public PersonaController(IPersonaRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            HostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return Ok(await Repository.All());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] PersonaDTO dto)
        {
            dto.DireccionFoto = await subirArchivo();
            return Ok(await Repository.Add(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] PersonaDTO dto)
        {
            dto.DireccionFoto = await subirArchivo();
            await Repository.Update(id, dto);
            return Ok();
        }

        private async Task<string> subirArchivo()
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Repository.Delete(id);
            return Ok();
        }
    }
}
