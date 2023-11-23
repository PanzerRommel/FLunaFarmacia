using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Pedidos
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var query = context.Pedidos.FromSqlRaw("PedidosGetAll").ToList();
                    result.Objects = new List<object>();
                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.Pedido pedido = new ML.Pedido()
                            {
                                IdPedido = obj.IdPedido,
                                Cliente = obj.Cliente
                            };
                            result.Objects.Add(pedido);
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
        public static ML.Result GetById(int? IdPedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var objquery = context.Pedidos.FromSqlRaw($"PedidosGetById {IdPedido}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Pedido pedido = new ML.Pedido();
                        pedido.IdPedido = objquery.IdPedido;
                        pedido.Cliente = objquery.Cliente;

                        result.Object = pedido;
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
        public static ML.Result Delete(int IdPedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var pedidoexist = context.Pedidos.FirstOrDefault(e => e.IdPedido == IdPedido);

                    if (pedidoexist != null)
                    {
                        context.Pedidos.Remove(pedidoexist);
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
        public static ML.Result Add(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PedidosAdd '{pedido.Cliente}'");
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
        public static ML.Result Update(ML.Pedido pedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new DL.FlunaControlFarmaciaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PedidosUpdate {pedido.IdPedido} , '{pedido.Cliente}'");
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
