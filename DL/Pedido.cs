using System;
using System.Collections.Generic;

namespace DL;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public string? Cliente { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();
}
