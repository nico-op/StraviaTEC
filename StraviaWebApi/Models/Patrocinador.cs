using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Patrocinador
{
    public string NombreLegal { get; set; } = null!;

    public string Logo { get; set; }

    public int Telefono { get; set; }

    public string NombreComercial { get; set; } = null!;


}
