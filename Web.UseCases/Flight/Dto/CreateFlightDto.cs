namespace Web.UseCases.Flight.Dto
{
    public class CreateFlightDto
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }
    }
}
