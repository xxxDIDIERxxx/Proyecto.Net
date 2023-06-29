using System;
using System.Collections.Generic;

namespace ASP.NETCoreIdentityCustom.Models
{
    public partial class Tarea
    {
        public int IdTarea { get; set; }
        public string NombreTarea { get; set; }
        public string DescripcionTarea { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public virtual Categoria? CategoriaNavigation { get; set; }
    }
}
