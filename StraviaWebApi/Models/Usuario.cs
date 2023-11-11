using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Usuario
{
    public string Nombre { get; set; }

    public string Apellido1 { get; set; }

    public string Apellido2 { get; set; }

    public DateTime FechaNacimiento { get; set; }

    public DateTime FechaActual { get; set; }

    public string Nacionalidad { get; set; }

    public string Foto { get; set; }

    public string NombreUsuario { get; set; }

    public string Contrasena { get; set; }

    public int Edad { get; set; }

}
