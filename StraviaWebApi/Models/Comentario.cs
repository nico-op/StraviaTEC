using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Comentario
{
    public string Contenido { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public string NombreUsuario { get; set; }

    public int ActividadId { get; set; }

    public int ComentarioId { get; set; }

}
