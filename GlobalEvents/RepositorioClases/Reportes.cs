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

    public partial class CommentsDetailsReport
    {
        public virtual Comments Comentario { get; set; }
        public virtual List<CommentsReportes> Reportes { get; set; }
    }

    public partial class EventsDetailsReport
    {
        public virtual Events Eventos { get; set; }
        public virtual List<EventsReportes> Reportes { get; set; }
    }
}
