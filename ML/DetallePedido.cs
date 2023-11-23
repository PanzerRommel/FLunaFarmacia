using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DetallePedido
    {
        public int IdDetalle { get; set; }
        public ML.Pedido Pedido { get; set; }
        public List<object> Pedidos { get; set; }
        public ML.Medicamento Medicamento { get; set; }
        public List<object> Medicamentos { get; set; }
        public int SKU { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        public List<object> DetallesPedidos { get; set; }
    }
}
