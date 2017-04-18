using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios
{
    public static class EventsService
    {
        /// <summary>
        /// Devuelve una lista de todos los eventos disponibles menos los eliminados.
        /// </summary>
        /// <returns>Eventos con cualquier estado menos eliminados.</returns>
        public static List<Events> ObtenerEventos()
        {
            using (Modelo context = new Modelo())
            {
                return context.Events.Where(z => z.Estado != EventState.Eliminado).ToList();
            }
        }
    }
}
