using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Reto
{
    public string Privacidad { get; set; }

    public int Periodo { get; set; }

    public string TipoActividad { get; set; }

    public string Altitud { get; set; }

    public string Fondo { get; set; }

    public string NombreReto { get; set; } = null!;

    public virtual ICollection<Patrocinador> NombreComercials { get; set; } = new List<Patrocinador>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
