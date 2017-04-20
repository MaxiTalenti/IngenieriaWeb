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

        /// <summary>
        /// Creación de usuario
        /// </summary>
        /// <param name="events"></param>
        public static void Create(Events events)
        {
            using (Modelo context = new Modelo())
            {
                context.Events.Add(new Events()
                {
                    Descripcion = events.Descripcion,
                    Estado = EventState.Habilitado,
                    Id = events.Id,
                    FechaFin = events.FechaFin,
                    FechaInicio = events.FechaInicio,
                    IdUser = events.IdUser,
                    lat = events.lat,
                    lng = events.lng,
                    NombreEvento = events.NombreEvento,
                    IdCategoria = events.IdCategoria,
                    Destacado = events.Destacado,
                    Direccion = events.Direccion
            });

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene usuarios menos lo que tienen estado eliminados.
        /// </summary>
        /// <param name="id">Busca por id (opcional)</param>
        /// <returns>Lista de usuario/s</returns>
        public static List<Events> Get(long? id)
        {
            using (Modelo context = new Modelo())
            {
                return context.Events.Where(u => id.HasValue ? id.Value == u.Id : true).ToList();
            }
        }

        /// <summary>
        /// Editar usuario
        /// </summary>
        /// <param name="user">Usuario a editar</param>
        public static void Edit(Events events)
        {
            using (Modelo context = new Modelo())
            {
                Events even = context.Events.Where(u => u.Id == events.Id).FirstOrDefault();
                if (even != null)
                {
                    even.Descripcion = events.Descripcion;
                    even.Estado = events.Estado;
                    even.Id = events.Id;
                    even.FechaFin = events.FechaFin;
                    even.FechaInicio = events.FechaInicio;
                    even.IdUser = events.IdUser;
                    even.lat = events.lat;
                    even.lng = events.lng;
                    even.NombreEvento = events.NombreEvento;
                    even.IdCategoria = events.IdCategoria;
                    even.Destacado = events.Destacado;
                    even.Direccion = events.Direccion;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Eliminar usuario (Marca como eliminado).
        /// </summary>
        /// <param name="user">Usuario a eliminar</param>
        public static void Delete(Events events)
        {
            using (Modelo context = new Modelo())
            {
                Events even = context.Events.Where(u => u.Id == events.Id).FirstOrDefault();

                // FirstOrDefault va a intentar recuperar el registro que cumpla la condición
                // si no encuentra ninguno, devuelve NULL, de ahí el siguiente IF.
                if (even != null)
                    even.Estado = EventState.Eliminado;

                // el objeto en memoria persiste los cambios en la base de datos cuando hago un save sobre el contexto.
                context.SaveChanges();
            }
        }
    }
}
