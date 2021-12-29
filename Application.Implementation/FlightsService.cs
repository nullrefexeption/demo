using Application.Interfaces;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Table;
using System.Linq.Dynamic.Core;

namespace Application.Implementation
{
    public class FlightsService : IFlightsService
    {
        private readonly IDbContext _dbContext;

        public FlightsService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TableData<Flight>> GetAllFlights(TableParams parameters)
        {
            TableData<Flight> result = new TableData<Flight>();

            var records = _dbContext.Flights
                .Include(x => x.ArrivalCity)
                .Include(x => x.DepartureCity)
                .AsNoTracking()
                .AsQueryable();

            if (parameters.Filters.Any())
                foreach (TableFilterItem filter in parameters.Filters)
                    records = filter.Field switch
                    {
                        "DDate" => records.Where(x => x.DepartureTime.Date == DateTime.Parse(filter.Value).Date),
                        "ADate" => records.Where(x => x.ArrivalTime.Date == DateTime.Parse(filter.Value).Date),
                        _ => records.Where($"{filter.Field} = \"{filter.Value}\"")
                    };

            if (!string.IsNullOrEmpty(parameters.SortField) && parameters.SortOrder != 0)
            {
                records = parameters.SortField switch
                {
                    "dcity" => parameters.SortOrder switch
                    {
                        -1 => records.OrderByDescending(x => x.DepartureCity.Name),
                        _ => records.OrderBy(x => x.DepartureCity.Name),
                    },
                    "acity" => parameters.SortOrder switch
                    {
                        -1 => records.OrderByDescending(x => x.ArrivalCity.Name),
                        _ => records.OrderBy(x => x.ArrivalCity.Name),
                    },
                    "dtime" => parameters.SortOrder switch
                    {
                        -1 => records.OrderByDescending(x => x.DepartureTime),
                        _ => records.OrderBy(x => x.DepartureTime),
                    },
                    "atime" => parameters.SortOrder switch
                    {
                        -1 => records.OrderByDescending(x => x.ArrivalTime),
                        _ => records.OrderBy(x => x.ArrivalTime),
                    },
                    _ => records.OrderBy(parameters.SortField + (parameters.SortOrder == -1 ? " descending" : string.Empty))
                };
            }

            if (parameters.Rows != 0)
            {
                records = records.Skip(parameters.First).Take(parameters.Rows);
            }

            result.Count = await records.CountAsync();

            result.Data = await records.ToListAsync();

            return result;
        }
    }
}
