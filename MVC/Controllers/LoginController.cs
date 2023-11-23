using Microsoft.AspNetCore.Mvc;

namespace TuProyecto.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            ML.Result result = BL.Usuario.ValidarUsuario(email, password);

            if (result.Correct)
            {
                // Autenticación exitosa, realiza acciones necesarias (por ejemplo, almacenar información del usuario en la sesión)
                // Redirige a la página principal u otra página deseada
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticación fallida, muestra mensaje de error o realiza otras acciones necesarias
                ViewBag.Mensaje = "Credenciales incorrectas. Inténtalo de nuevo.";
                return View();
            }
        }
    }
}
