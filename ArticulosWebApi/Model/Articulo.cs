using System;
using System.Collections.Generic;

namespace ArticulosWebApi.Model;

public partial class Articulo
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Precio { get; set; }
}
