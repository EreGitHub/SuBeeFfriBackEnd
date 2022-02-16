using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuBeefrri.Core.Dtos;
using SuBeefrri.Services.Interfaces;

namespace SuBeefrri.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdenPedidoController : ControllerBase
    {
        private readonly IOrdenPedidoRepository Repository;
        public OrdenPedidoController(IOrdenPedidoRepository repository)
        {
            Repository = repository;
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
        public async Task<IActionResult> Orden(OrdenPedidoDTO dto)
        {
            var oOrden = await Repository.Orden(dto);
            return Ok(oOrden);
        }

        [HttpPost]
        public async Task<IActionResult> Cobrar(int idPedido, int idUsuarioCobrador)
        {
            await Repository.Cobrar(idPedido, idUsuarioCobrador);
            return Ok();
        }

        [HttpPost("{idPedido}")]
        public async Task<IActionResult> Aprobar(int idPedido)
        {
            await Repository.Aprobar(idPedido);
            return Ok();
        }

        [HttpPost("{idPedido}")]
        public async Task<IActionResult> Rechazar(int idPedido)
        {
            await Repository.Rechazar(idPedido);
            return Ok();
        }
    }
}
