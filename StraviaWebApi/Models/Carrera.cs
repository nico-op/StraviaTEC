using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Carrera
{
    public int Costo { get; set; }

    public string Modalidad { get; set; } = null!;

    public DateTime FechaCarrera { get; set; }

    public string Recorrido { get; set; }

    public string NombreCarrera { get; set; } = null!;

    public virtual ICollection<Categorium> Categoria { get; set; } = new List<Categorium>();

    public virtual ICollection<CuentasBancaria> CuentasBancaria { get; set; } = new List<CuentasBancaria>();

    public virtual ICollection<Patrocinador> NombreComercials { get; set; } = new List<Patrocinador>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
