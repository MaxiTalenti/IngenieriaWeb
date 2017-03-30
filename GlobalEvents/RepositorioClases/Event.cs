using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioClases
{
    public class Event
    {
        public long id { get; set; }
        public string EventName { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
