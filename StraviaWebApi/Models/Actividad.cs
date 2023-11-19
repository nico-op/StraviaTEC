using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Actividad
{
    public int ActividadId { get; set; }

    public string TipoActividad { get; set; }

    public int Kilometraje { get; set; }

    public int Altitud { get; set; }

    public string Ruta { get; set; }

    public DateTime FechaHora { get; set; }

    public int Duracion { get; set; }

    public string NombreUsuario { get; set; }
}
