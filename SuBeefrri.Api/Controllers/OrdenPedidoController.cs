using Microsoft.AspNetCore.Mvc;
using SuBeefrri.Api.DTOs;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdenPedidoController : ControllerBase
    {
        private readonly IOrdenPedidoRepository Repository;
        private readonly IWebHostEnvironment HostEnvironment;
        public OrdenPedidoController(IOrdenPedidoRepository repository, IWebHostEnvironment hostEnvironment)
        {
            Repository = repository;
            HostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> ListarOrdenes()
        {
            return Ok(await Repository.ListarOrdenes());
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ListarOrdenesPorUsuario(int idUsuario)
        {
            return Ok(await Repository.ListarOrdenesPorUsuario(idUsuario));
        }

        [HttpPost]
        public async Task<IActionResult> OrdenSinPago(OrdenPedidoDTO dto)
        {
            var oOrden = await Repository.OrdenarSinPago(dto);
            return Ok(oOrden);
        }

        [HttpPost]
        public async Task<IActionResult> OrdenConPago(OrdenPedidoDTO dto)
        {
            var result = await Repository.OrdenConPago(dto);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AdjuntarComprobantePago([FromForm] AdjuntarComprobantePagoDTO dto)
        {
            var DireccionFoto = await subirArchivo();
            await Repository.AdjuntarOrdenPago(new OrdenConPagoAdjuntoDTO
            {
                NumeroTransferencia = dto.NumeroTransferencia,
                NombreBanco = dto.NombreBanco,
                Monto = dto.Monto,
                DireccionFoto = DireccionFoto,
                IdOrderPedido = dto.IdOrderPedido
            });
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Cobrar(int idPedido, int idUsuarioCobrador)
        {
            await Repository.Cobrar(idPedido, idUsuarioCobrador);
            return Ok();
        }

        [HttpPost("{idPedido}")]
        public async Task<IActionResult> Enviar(int idPedido)
        {
            await Repository.Enviada(idPedido);
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
    }
}
