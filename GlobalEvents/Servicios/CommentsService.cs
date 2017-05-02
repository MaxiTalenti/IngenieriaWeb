using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositorioClases;

namespace Servicios
{
    public static class CommentsService
    {
        public static List<Comments> ObtenerEventos()
        {
            using (Modelo context = new Modelo())
            {
                //return context.co.Where(z => z.Estado != EventState.Eliminado).ToList();
            }
        }
    }
}
