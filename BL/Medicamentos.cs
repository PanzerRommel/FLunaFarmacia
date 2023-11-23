using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medicamentos
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var query = context.Medicamentos.FromSqlRaw("MedicamentoGetAll").ToList();
                    result.Objects = new List<object>();
                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento()
                            {
                                IdMedicamento = obj.IdMedicamento,
                                SKU = obj.Sku.Value,
                                Precio = obj.Precio.Value,
                                Nombre = obj.Nombre
                            };
                            result.Objects.Add(medicamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int? IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var objquery = context.Medicamentos.FromSqlRaw($"MedicamentoGetById {IdMedicamento}").AsEnumerable().FirstOrDefault();
                    if(objquery != null)
                    {
                        ML.Medicamento medicamento = new ML.Medicamento();
                        medicamento.IdMedicamento = objquery.IdMedicamento;
                        medicamento.SKU = objquery.Sku.Value;
                        medicamento.Precio = objquery.Precio.Value;
                        medicamento.Nombre = objquery.Nombre;

                        result.Object = medicamento;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var medicamentoID = context.Medicamentos.FirstOrDefault(e => e.IdMedicamento == IdMedicamento);
                    if (medicamentoID != null)
                    {
                        context.Medicamentos.Remove(medicamentoID);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontró el empleado para eliminar.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedicamentoAdd {medicamento.SKU} , {medicamento.Precio} , '{medicamento.Nombre}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"MedicamentoUpdate {medicamento.IdMedicamento} , {medicamento.SKU} , {medicamento.Precio} , '{medicamento.Nombre}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
