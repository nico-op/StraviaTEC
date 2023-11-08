using System;
using System.Collections.Generic;



namespace StraviaWebApi.Models;

public partial class Actividad
{
    public string TipoActividad { get; set; }

    public int Kilometraje { get; set; }

    public int Altitud { get; set; }

    public string Ruta { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan Hora { get; set; }

    public TimeSpan Duracion { get; set; }

    public int ActividadId { get; set; }

    public string Usuario { get; set; }

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Usuario UsuarioNavigation { get; set; }
}
