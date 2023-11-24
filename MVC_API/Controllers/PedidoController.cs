using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        // GET: api/Pedido
        public ActionResult<IEnumerable<ML.Pedido>> GetAll()
        {
            ML.Pedido pedido = new ML.Pedido();
            ML.Result result = BL.Pedidos.GetAll();
            if (result.Correct)
            {
                pedido.Pedidos = result.Objects;
                return Ok(pedido); // Devuelve 200 OK junto con los datos
            }
            return BadRequest(); // Puedes personalizar el mensaje de error según tus necesidades
        }

        [HttpGet("{id}")]
        // GET: api/Pedido/5
        public ActionResult<ML.Pedido> Form(int id)
        {
            ML.Pedido pedido = new ML.Pedido();
            if (id <= 0)
            {
                return BadRequest(); // Puedes personalizar el mensaje de error según tus necesidades
            }
            else
            {
                ML.Result result = BL.Pedidos.GetById(id);

                if (result.Correct)
                {
                    pedido = ((ML.Pedido)result.Object);
                    return Ok(pedido); // Devuelve 200 OK junto con los datos
                }
                else
                {
                    return NotFound(); // Devuelve 404 Not Found si el recurso no se encuentra
                }
            }
        }

        [HttpPost]
        // POST: api/Pedido
        public ActionResult Form([FromBody] ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();
            if (pedido.IdPedido == 0)
            {
                result = BL.Pedidos.Add(pedido);
                if (result.Correct)
                {
                    return Ok(result); // Devuelve 200 OK junto con los datos
                }
                else
                {
                    return BadRequest(result); // Devuelve 400 Bad Request con el mensaje de error
                }
            }
            else
            {
                result = BL.Pedidos.Update(pedido);
                if (result.Correct)
                {
                    return Ok(result); // Devuelve 200 OK junto con los datos
                }
                else
                {
                    return BadRequest(result); // Devuelve 400 Bad Request con el mensaje de error
                }
            }
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ML.Result result = new ML.Result();
            result = BL.Pedidos.Delete(id);
            if (result.Correct)
            {
                return Ok(result); // Devuelve 200 OK junto con los datos
            }
            else
            {
                return BadRequest(result); // Devuelve 400 Bad Request con el mensaje de error
            }
        }
    }
}
