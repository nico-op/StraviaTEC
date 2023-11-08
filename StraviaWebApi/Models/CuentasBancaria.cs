using System;
using System.Collections.Generic;

namespace StraviaWebApi.Models;

public partial class CuentasBancaria
{
    public string NombreCarrera { get; set; } = null!;

    public string NombreBanco { get; set; }

    public string NumeroCuenta { get; set; } = null!;


}
