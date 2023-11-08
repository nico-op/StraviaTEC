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

    public virtual ICollection<Actividad> Actividads { get; set; } = new List<Actividad>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Grupo> Grupos { get; set; } = new List<Grupo>();

    public virtual ICollection<Carrera> NombreCarreras { get; set; } = new List<Carrera>();

    public virtual ICollection<Reto> NombreRetos { get; set; } = new List<Reto>();

    public virtual ICollection<Usuario> UsuarioDestinos { get; set; } = new List<Usuario>();

    public virtual ICollection<Usuario> UsuarioOrigens { get; set; } = new List<Usuario>();
}
