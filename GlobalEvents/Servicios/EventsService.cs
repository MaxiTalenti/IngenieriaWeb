using RepositorioClases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;


namespace Servicios
{
    public static class EventsService
    {
        public static List<Event> ObtenerEventos()
        {
            using (Entities context = new Entities())
            {
                return context.Events.Select(u => new Event()
                {
                    EventName = u.EventName,
                    id = u.Id,
                    lat = u.lat,
                    lng = u.lng
                }).ToList();
            }
        }
    }
}
