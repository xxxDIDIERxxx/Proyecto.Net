using System;
using System.Collections.Generic;

namespace ASP.NETCoreIdentityCustom.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Tareas = new HashSet<Tarea>();
        }

        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = null!;
        public string? ColorCategoria { get; set; }

        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
