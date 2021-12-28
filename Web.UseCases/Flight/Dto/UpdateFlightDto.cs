namespace Web.UseCases.Flight.Dto
{
    public class UpdateFlightDto
    {
        public int Id { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public int DepartureCityId { get; set; }
        public int ArrivalCityId { get; set; }

        public long Delay { get; set; }
    }
}
