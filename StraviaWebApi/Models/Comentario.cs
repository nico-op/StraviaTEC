using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Comentario
{
    public string Contenido { get; set; } = null!;

    public DateTime? FechaPublicacion { get; set; }

    public string Usuario { get; set; }

    public int ActividadId { get; set; }

    public int ComentarioId { get; set; }


}
