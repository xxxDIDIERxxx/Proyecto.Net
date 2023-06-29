using System;
using System.Collections.Generic;

namespace ASP.NETCoreIdentityCustom.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        public int Id { get; set; }
        public string Cedula { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
