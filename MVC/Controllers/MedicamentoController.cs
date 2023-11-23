using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class MedicamentoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            ML.Result result = BL.Medicamentos.GetAll();
            if (result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
            }
            return View(medicamento);
        }
        [HttpGet]
        public ActionResult Form(int? IdMedicamento)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            if (IdMedicamento == null)
            {
                return View(medicamento);
            }
            else
            {
                ML.Result result = BL.Medicamentos.GetById(IdMedicamento.Value);

                if (result.Correct)
                {
                    medicamento = ((ML.Medicamento)result.Object);

                    return View(medicamento);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }

            }
        }

        [HttpPost]
        public ActionResult Form(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            if (medicamento.IdMedicamento == 0)
            {
                result = BL.Medicamentos.Add(medicamento);
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
                result = BL.Medicamentos.Update(medicamento);
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
            return View(medicamento);
        }
        public ActionResult Delete(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            result = BL.Medicamentos.Delete(IdMedicamento);
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
