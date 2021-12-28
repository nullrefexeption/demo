using Application.Interfaces;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var records = _dbContext.Flights.AsQueryable();

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
