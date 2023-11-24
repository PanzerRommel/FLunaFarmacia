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
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var query = context.DetallesPedidos.FromSql(FormattableStringFactory.Create("DetallesPedidoGetAll")).ToList();

                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var detallePedidoDB in query)
                        {
                            ML.DetallePedido detallePedido = new ML.DetallePedido()
                            {
                                IdDetalle = detallePedidoDB.IdDetalle,
                                Pedido = new ML.Pedido
                                {
                                    IdPedido = detallePedidoDB.IdPedido.Value,
                                    Cliente = detallePedidoDB.Cliente
                                },
                                Medicamento = new ML.Medicamento()
                                {
                                    IdMedicamento = detallePedidoDB.IdMedicamento.Value,
                                    Nombre = detallePedidoDB.NombreMedicamento
                                },
                                SKU = detallePedidoDB.Sku.Value,
                                Cantidad = detallePedidoDB.Cantidad.Value,
                                PrecioUnitario = detallePedidoDB.PrecioUnitario.Value,
                                Total = detallePedidoDB.Total.Value
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
                result.ErrorMessage = $"Error al obtener detalles de pedidos: {ex.Message}";
                // Loguea la excepción o toma medidas adicionales según tus necesidades
            }
            return result;
        }
        public static ML.Result Add(ML.DetallePedido detallePedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    // Verifica si el pedido y el medicamento existen antes de agregar el detalle
                    var existingPedido = context.Pedidos.Find(detallePedido.Pedido.IdPedido);
                    var existingMedicamento = context.Medicamentos.Find(detallePedido.Medicamento.IdMedicamento);

                    if (existingPedido == null || existingMedicamento == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El pedido o el medicamento asociado no existe.";
                    }
                    else
                    {
                        // Crea una nueva instancia de DetallesPedido
                        DL.DetallesPedido detallesPedidoDB = new DL.DetallesPedido
                        {
                            IdPedido = detallePedido.Pedido.IdPedido,
                            IdMedicamento = detallePedido.Medicamento.IdMedicamento,
                            Sku = detallePedido.SKU,
                            Cantidad = detallePedido.Cantidad,
                            PrecioUnitario = detallePedido.PrecioUnitario,
                            Total = detallePedido.Total
                        };

                        // Agrega el nuevo DetallesPedido a la base de datos
                        context.DetallesPedidos.Add(detallesPedidoDB);
                        context.SaveChanges();

                        result.Correct = true;
                        result.Object = detallesPedidoDB.IdDetalle; // Puedes devolver el IdDetalle generado si es necesario
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = $"Error al agregar detalle de pedido: {ex.Message}";
                // Loguea la excepción o toma medidas adicionales según tus necesidades
            }
            return result;
        }
        public static ML.Result Update(ML.DetallePedido detallePedido)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    // Verifica si el DetallePedido existe antes de intentar actualizarlo
                    var existingDetallePedido = context.DetallesPedidos.Find(detallePedido.IdDetalle);

                    if (existingDetallePedido == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El DetallePedido no existe.";
                    }
                    else
                    {
                        // Actualiza las propiedades del DetallePedido existente
                        existingDetallePedido.Sku = detallePedido.SKU;
                        existingDetallePedido.Cantidad = detallePedido.Cantidad;
                        existingDetallePedido.PrecioUnitario = detallePedido.PrecioUnitario;
                        existingDetallePedido.Total = detallePedido.Total;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = $"Error al actualizar detalle de pedido: {ex.Message}";
                // Loguea la excepción o toma medidas adicionales según tus necesidades
            }
            return result;
        }
        public static ML.Result Delete(int idDetalle)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    // Verifica si el DetallePedido existe antes de intentar eliminarlo
                    var existingDetallePedido = context.DetallesPedidos.Find(idDetalle);

                    if (existingDetallePedido == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El DetallePedido no existe.";
                    }
                    else
                    {
                        // Elimina el DetallePedido existente
                        context.DetallesPedidos.Remove(existingDetallePedido);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = $"Error al eliminar detalle de pedido: {ex.Message}";
                // Loguea la excepción o toma medidas adicionales según tus necesidades
            }
            return result;
        }
        public static ML.Result GetById(int idDetalle)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaControlFarmaciaContext context = new FlunaControlFarmaciaContext())
                {
                    var detallePedidoDB = context.DetallesPedidos.Find(idDetalle);

                    if(detallePedidoDB == null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "El detalle del pedido no existe";
                    }
                    else
                    {
                        ML.DetallePedido detallePedido = new ML.DetallePedido()
                        {
                            IdDetalle = detallePedidoDB.IdDetalle,
                            Pedido = new ML.Pedido
                            {
                                IdPedido = detallePedidoDB.IdPedido.Value,
                                Cliente = detallePedidoDB.Cliente
                            },
                            Medicamento = new ML.Medicamento()
                            {
                                IdMedicamento = detallePedidoDB.IdMedicamento.Value,
                                Nombre = detallePedidoDB.NombreMedicamento
                            },
                            SKU = detallePedidoDB.Sku.Value,
                            Cantidad = detallePedidoDB.Cantidad.Value,
                            PrecioUnitario = detallePedidoDB.PrecioUnitario.Value,
                            Total = detallePedidoDB.Total.Value
                        };
                        result.Object = detallePedido;
                        result.Correct = true;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = $"Error al obtener detalle de pedido por ID: {ex.Message}";
            }
            return result;
        }
    }
}
