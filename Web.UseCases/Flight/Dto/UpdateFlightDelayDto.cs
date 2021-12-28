using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.UseCases.Flight.Dto
{
    public class UpdateFlightDelayDto
    {
        public int Id { get; set; }
        public int Delay { get; set; }
    }
}
