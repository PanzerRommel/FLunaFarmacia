using DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DetallePedido
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var query = context.DetallesPedidos.FromSql(FormattableStringFactory.Create("DetallesPedidoGetAll")).ToList();

                    result.Objects = new List<object>();
                    if(query != null)
                    {
                        foreach(var obj in query)
                        {
                            ML.DetallePedido detallePedido = new ML.DetallePedido()
                            {
                                IdDetalle = obj.IdDetalle,
                                Pedido = new ML.Pedido
                                {
                                    IdPedido = obj.IdPedido.Value

                                },
                                Medicamento = new ML.Medicamento()
                                {
                                    Nombre = obj.NombreMedicamento
                                },
                                SKU = obj.Sku.Value,
                                Cantidad = obj.Cantidad.Value,
                                PrecioUnitario = obj.PrecioUnitario.Value,
                                Total =obj.Total.Value

                                
                            };
                            result.Objects.Add(detallePedido);
                        }
                        result.Correct = true;
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
