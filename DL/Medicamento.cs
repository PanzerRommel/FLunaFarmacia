using System;
using System.Collections.Generic;

namespace DL;

public partial class Medicamento
{
    public int IdMedicamento { get; set; }

    public int? Sku { get; set; }

    public decimal? Precio { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();
}
