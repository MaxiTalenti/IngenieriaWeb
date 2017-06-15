using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RepositorioClases;
using Servicios;

namespace GlobalEvents.Controllers
{
    public class EventsApiController : ApiController
    {
        // GET: api/EventsApi
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/EventsApi/5
        public void Get(int id)
        {
            Events evento = EventsService.ObtenerEventos((long)id).SingleOrDefault();
            if (evento != null)
                Request.CreateResponse(HttpStatusCode.Accepted, evento);
        }

        // POST: api/EventsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/EventsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EventsApi/5
        public void Delete(int id)
        {
        }
    }
}
