using Microsoft.AspNetCore.Mvc;
using DL;

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
                // Verifica que el objeto devuelto por ValidarUsuario sea de tipo DL.Usuario
                if (result.Object is DL.Usuario usuarioAutenticadoDL)
                {
                    // Crea un objeto ML.Usuario a partir del objeto DL.Usuario
                    ML.Usuario usuarioAutenticadoML = new ML.Usuario
                    {
                        IdUsuario = usuarioAutenticadoDL.IdUsuario,
                        Nombre = usuarioAutenticadoDL.Nombre,
                        Email = usuarioAutenticadoDL.Email,
                        Password = usuarioAutenticadoDL.Password
                    };

                    // Puedes realizar acciones adicionales aquí, pero no se está utilizando la sesión en este ejemplo.

                    // Autenticación exitosa, redirige a la página principal u otra página deseada
                    return RedirectToAction("GetAll", "Pedido");
                }
                else
                {
                    // Manejar el caso en que el objeto devuelto no es de tipo DL.Usuario
                    ViewBag.Mensaje = "Error en la autenticación. Inténtalo de nuevo.";
                    return View();
                }
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

