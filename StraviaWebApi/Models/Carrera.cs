using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Carrera
{
    public int Costo { get; set; }

    public string Modalidad { get; set; }

    public DateTime FechaCarrera { get; set; }

    public string Recorrido { get; set; }

    public string NombreCarrera { get; set; }
}
