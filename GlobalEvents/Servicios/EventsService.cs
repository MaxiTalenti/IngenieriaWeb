using Hopac.Core;
using RepositorioClases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

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
        public static void Create(Events events, HttpPostedFileBase file, TimeSpan HoraInicio, TimeSpan HoraFin)
        {
            String uriimage = null;
            if (file != null)
            {
                var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
                string savedFileName = Path.Combine(path, Path.GetFileName(file.FileName));
                file.SaveAs(savedFileName);

                Servicios.Imagenes imagenes = new Imagenes();
                ImageUploadResult result = imagenes.subirImagen(savedFileName);
                if (result.Status == "OK")
                {
                    uriimage = result.Uri;
                }

                File.Delete(savedFileName);
            }
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
                    Direccion = events.Direccion,
                    RutaImagen = uriimage,
                    HoraInicio = events.HoraInicio,
                    HoraFin = events.HoraFin
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
                List<Events> Eventos = context.Events.Where(u => id.HasValue ? id.Value == u.Id : true).ToList();
                if (id != null)
                    Eventos.FirstOrDefault().Comments = CommentsService.ObtenerComentarios(id.GetValueOrDefault()).ToList();
                return Eventos;
            }
        }

        public static List<Events> GetForMap(long? id)
        {
            using (Modelo context = new Modelo())
            {
                return context.Events.Where(u => id.HasValue ? id.Value == u.Id : true && u.Estado == EventState.Habilitado && u.lat != null).ToList();
            }
        }

        /// <summary>
        /// Editar usuario
        /// </summary>
        /// <param name="user">Usuario a editar</param>
        public static void Edit(Events events, HttpPostedFileBase file, TimeSpan HoraInicio, TimeSpan HoraFin)
        {
            String uriimage = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data");
                    string savedFileName = Path.Combine(path, Path.GetFileName(file.FileName));
                    file.SaveAs(savedFileName);

                    Servicios.Imagenes imagenes = new Imagenes();
                    ImageUploadResult result = imagenes.subirImagen(savedFileName);
                    if (result.Status == "OK")
                    {
                        uriimage = result.Uri;
                    }

                    File.Delete(savedFileName);
                }
            }
            using (Modelo context = new Modelo())
            {
                Events even = context.Events.Where(u => u.Id == events.Id).FirstOrDefault();
                if (uriimage == null && events.RutaImagen != null)
                {
                    uriimage = events.RutaImagen;
                }
                if (even != null)
                {
                    even.Descripcion = events.Descripcion;
                    even.Estado = events.Estado;
                    even.Id = events.Id;
                    even.FechaFin = events.FechaFin;
                    even.FechaInicio = events.FechaInicio;
                    even.IdUser = even.IdUser;
                    even.lat = events.lat;
                    even.lng = events.lng;
                    even.NombreEvento = events.NombreEvento;
                    even.IdCategoria = events.IdCategoria;
                    even.Destacado = events.Destacado;
                    even.Direccion = events.Direccion;
                    even.RutaImagen = uriimage;
                    even.HoraInicio = events.HoraInicio;
                    even.HoraFin = events.HoraFin;
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

        /// <summary>
        /// Cambiar el estado de un evento
        /// </summary>
        /// <param name="EventId"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public static bool CambiarEstadoEvento(long EventId, EventState estado)
        {
            using (Modelo context = new Modelo())
            {
                Events evento = context.Events.SingleOrDefault(c => c.Id == EventId);
                evento.Estado = estado;
                context.SaveChanges();
            }
            return true;
        }

        public static bool InteresesEventos(int EventId, int UserId, Intereses Tipo, bool Anular = false)
        {
            using (Modelo context = new Modelo())
            {
                InteresesEventos interes = context.InteresesEventos
                        .Where(z => z.EventId == EventId)
                        .Where(z => z.Tipo == Tipo)
                        .SingleOrDefault(c => c.UserId == UserId);

                if (interes == null)
                {
                    // No existe este interés.
                    context.InteresesEventos.Add(new InteresesEventos()
                    {
                        EventId = EventId,
                        UserId = UserId,
                        Tipo = Tipo,
                        Fecha = DateTime.Now,
                        Anulado = false
                    });
                    context.SaveChanges();
                }
                else
                {
                    // Ya existe el interes, cambiar el estado.
                    interes.Anulado = Anular;
                    if (Anular)
                        interes.FechaAnulacion = DateTime.Now;
                    context.SaveChanges();
                }
            }
            return true;
        }


    }
}
