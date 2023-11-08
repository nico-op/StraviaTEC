using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Usuario
{
    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaActual { get; set; }

    public string Nacionalidad { get; set; } = null!;

    public string Foto { get; set; }

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int Edad { get; set; }
}
