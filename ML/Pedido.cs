using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string Cliente { get; set; }

        public List<object> Pedidos { get; set; }
    }
}
