using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallePedidoController : ControllerBase
    {
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<ML.DetallePedido>> GetAll()
        {
            ML.DetallePedido detallePedido = new ML.DetallePedido();
            ML.Result result = BL.DetallePedido.GetAll();
            if (result.Correct)
            {
                detallePedido.DetallesPedidos = result.Objects;
            }
            return Ok(detallePedido.DetallesPedidos);
        }
    }
}
