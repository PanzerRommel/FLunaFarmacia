using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    namespace MVC.Controllers
    {
        public class DetallePedidoController : Controller
        {
            [HttpGet]
            // GET: PedidoController
            public ActionResult GetAll()
            {
                ML.DetallePedido detallePedido = new ML.DetallePedido();
                ML.Result result = BL.DetallePedido.GetAll();
                if (result.Correct)
                {
                    detallePedido.DetallesPedidos = result.Objects;
                }
                return View(detallePedido);
            }

            [HttpGet]
            public ActionResult Form(int? idDetallePedido)
            {
                ML.DetallePedido detallePedido = new ML.DetallePedido();
                if (idDetallePedido.HasValue)
                {
                    ML.Result result = BL.DetallePedido.GetById(idDetallePedido.Value);
                    if (result.Correct)
                    {
                        detallePedido = ((ML.DetallePedido)result.Object);
                        return View(detallePedido);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                        return View("Modal");
                    }
                }
                else
                {
                    return View(detallePedido);
                }
            }

            [HttpPost]
            public ActionResult Form(ML.DetallePedido detallePedido)
            {
                ML.Result result = new ML.Result();
                if(detallePedido.IdDetalle == 0)
                {
                    result = BL.DetallePedido.Add(detallePedido);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Registro exitoso";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un problema";
                    }
                    return PartialView("Modal");
                }
                else
                {
                    result = BL.DetallePedido.Update(detallePedido);
                    if (result.Correct)
                    {
                        ViewBag.Mensaje = "Actualizacion exitoso";
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un problema";
                    }
                    return View(detallePedido);
                }
            }
            public ActionResult Delete(int idDetalle)
            {
                ML.Result result = new ML.Result();
                result = BL.DetallePedido.Delete(idDetalle);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro eliminado";
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                }
                return PartialView("Modal");
            }
        }
    }

}

