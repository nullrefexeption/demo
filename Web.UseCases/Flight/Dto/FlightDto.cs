using Web.UseCases.City.Dto;

namespace Web.UseCases.Flight.Dto
{
    public class FlightDto
    {
        public int Id { get; set; }

        public CityDto DepartureCity { get; set; }

        public CityDto ArrivalCity { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int Delay { get; set; }
    }
}
