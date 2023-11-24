using Microsoft.AspNetCore.Mvc;

namespace TuProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Authenticate([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.ValidarUsuario(usuario.Email, usuario.Password);

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

                    // Autenticación exitosa, devolver los datos del usuario autenticado
                    return Ok(usuarioAutenticadoML);
                }
                else
                {
                    // Manejar el caso en que el objeto devuelto no es de tipo DL.Usuario
                    return BadRequest("Error en la autenticación. Inténtalo de nuevo.");
                }
            }
            else
            {
                // Autenticación fallida, devolver mensaje de error
                return BadRequest("Credenciales incorrectas. Inténtalo de nuevo.");
            }
        }
    }
}
