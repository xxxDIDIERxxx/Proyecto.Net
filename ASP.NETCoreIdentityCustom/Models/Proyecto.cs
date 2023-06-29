using System;
using System.Collections.Generic;

namespace ASP.NETCoreIdentityCustom.Models
{
    public partial class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string CedulaCliente { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int TipoRiesgo { get; set; }

        public virtual Cliente CedulaClienteNavigation { get; set; } = null!;
    }
}
