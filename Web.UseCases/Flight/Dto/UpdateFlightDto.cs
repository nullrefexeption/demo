using Web.UseCases.City.Dto;

namespace Web.UseCases.Flight.Dto
{
    public class UpdateFlightDto
    {
        public int Id { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public CityDto ArrivalCity { get; set; }
        public CityDto DepartureCity { get; set; }

        public long Delay { get; set; }
    }
}
