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
        public static List<Events> ObtenerEventos()
        {
            using (Modelo context = new Modelo())
            {

                return (from u in context.Events
                                select new Events()
                                {
                                    EventName = u.EventName,
                                    Id = u.Id,
                                    lat = u.lat,
                                    lng = u.lng
                                }).ToList();
            }
        }
    }
}
