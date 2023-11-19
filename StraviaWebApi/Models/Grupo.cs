using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class Grupo
{
    public string NombreGrupo { get; set; }

    public string Descripcion { get; set; }

    public string Administrador { get; set; }

    public DateTime? Creacion { get; set; }

    public string GrupoId { get; set; }


}
