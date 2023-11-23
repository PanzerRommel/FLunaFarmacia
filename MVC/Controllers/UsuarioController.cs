using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();
            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object);
                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);
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
                result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualización exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al actualizar el usuario";
                }
                return PartialView("Modal");
            }
        }

        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Usuario Eliminado con Éxito";
            }
            else
            {
                ViewBag.Mensaje = "Se ha producido un error" + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
    }
}
