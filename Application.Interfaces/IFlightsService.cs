using Models.Models;
using Models.Models.Table;

namespace Application.Interfaces
{
    public interface IFlightsService
    {
        Task<TableData<Flight>> GetAllFlights(TableParams tableParams);
    }
}
