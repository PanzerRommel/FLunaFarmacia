using System;
using System.Collections.Generic;

namespace DL;

public partial class DetallesPedido
{
    public int IdDetalle { get; set; }

    public int? IdPedido { get; set; }
    public string? Cliente { get; set; }

    public int? IdMedicamento { get; set; }
    public string? NombreMedicamento { get; set; }

    public int? Sku { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioUnitario { get; set; }

    public decimal? Total { get; set; }

    public virtual Medicamento? IdMedicamentoNavigation { get; set; }

    public virtual Pedido? IdPedidoNavigation { get; set; }
}
