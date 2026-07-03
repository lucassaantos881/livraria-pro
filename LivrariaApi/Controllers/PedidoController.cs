using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LivrariaCore.Models;
using LivrariaCore.DTO_s;
using LivrariaApi.Services;

namespace LivrariaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {

        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public ActionResult<Pedido> Post([FromBody] PedidoDTO pedidoDto)
        {

            if (pedidoDto == null)
            {
                return BadRequest();
            }

            _pedidoService.AdicionarPedido(pedidoDto);
            return CreatedAtAction(nameof(Post), new { id = pedidoDto.IdPedido }, pedidoDto);


        }

        [HttpPut("despachar/{id}")]
        public ActionResult<Pedido> Put(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            _pedidoService.DespacharPedido(id);
            return NoContent();
        }

        [HttpGet ("{id}")]
        public ActionResult<double> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var pedido = _pedidoService.CalcularPedido(id);
            return Ok(pedido);
        }

        [HttpGet ("consultar/{id}")]
        public ActionResult<Pedido> GetLocalizarPedido(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }


            var pedidoLocalizado = _pedidoService.AnalisarPedido(id);
            return Ok(pedidoLocalizado);

        }

        [HttpGet]
        public ActionResult<IEnumerable<Pedido>> GetPedidos()
        {
            return _pedidoService.ListarPedidos().ToList();
        }

        [HttpDelete("{id}")]
        public ActionResult<Pedido> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            _pedidoService.CancelarPedido(id);
            return NoContent();
        }


    }
}
