using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Flight> OutgoingFlights { get; set; }
        public ICollection<Flight> IncomingFlights { get; set; }
    }
}
