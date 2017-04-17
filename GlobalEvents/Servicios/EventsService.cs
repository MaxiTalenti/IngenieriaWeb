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
        /// Devuelve una lista de todos los eventos disponibles.
        /// </summary>
        /// <returns>Eventos</returns>
        public static List<Events> ObtenerEventos()
        {
            using (Modelo context = new Modelo())
            {

                return context.Events.ToList();
            }
        }
    }
}
