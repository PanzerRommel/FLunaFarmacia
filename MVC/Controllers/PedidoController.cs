using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class PedidoController : Controller
    {
        [HttpGet]
        // GET: PedidoController
        public ActionResult GetAll()
        {
            ML.Pedido pedido = new ML.Pedido();
            ML.Result result = BL.Pedidos.GetAll();
            if (result.Correct)
            {
                pedido.Pedidos = result.Objects;
            }
            return View(pedido);
        }
        [HttpGet]
        // GET: PedidoController/Details/5
        public ActionResult Form(int? IdPedido)
        {
            ML.Pedido pedido = new ML.Pedido();
            if(IdPedido == null)
            {
                return View(pedido);
            }
            else
            {
                ML.Result result = BL.Pedidos.GetById(IdPedido.Value);

                if (result.Correct)
                {
                    pedido = ((ML.Pedido)result.Object);
                    return View(pedido);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        // GET: PedidoController/Create
        public ActionResult Form(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();
            if(pedido.IdPedido == 0)
            {
                result = BL.Pedidos.Add(pedido);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro de manera exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al agregar el registro";
                }
                return PartialView("Modal");
            }
            else
            {
                result = BL.Pedidos.Update(pedido);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualizacion extisosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un problema al actualizar descripcion";
                }
                return PartialView("Modal");
            }
            return View(pedido);
        }
  
        // GET: PedidoController/Delete/5
        public ActionResult Delete(int IdPedido)
        {
            ML.Result result = new ML.Result();
            result = BL.Pedidos.Delete(IdPedido);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado Con Exitoso";
            }
            else
            {
                ViewBag.Mensaje = "Se A Producido Un Error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}
