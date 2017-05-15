using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public partial class Reportes
    {
        public virtual List<CommentsReportes> Comentarios { get; set;}
        public virtual List<EventsReportes> Eventos { get; set; }
    }
}
