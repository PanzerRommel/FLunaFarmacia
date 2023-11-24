using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ML.Medicamento>> GetAll()
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            ML.Result result = BL.Medicamentos.GetAll();
            if (result.Correct)
            {
                medicamento.Medicamentos = result.Objects;
                return Ok(medicamento);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public ActionResult<ML.Medicamento> Form(int id)
        {
            ML.Medicamento medicamento = new ML.Medicamento();
            if (id <= 0)
            {
                return BadRequest();
            }
            else
            {
                ML.Result result = BL.Medicamentos.GetById(id);

                if (result.Correct)
                {
                    medicamento = ((ML.Medicamento)result.Object);
                    return Ok(medicamento);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult Form([FromBody] ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            if (medicamento.IdMedicamento == 0)
            {
                result = BL.Medicamentos.Add(medicamento);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                result = BL.Medicamentos.Update(medicamento);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            ML.Result result = new ML.Result();
            result = BL.Medicamentos.Delete(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
