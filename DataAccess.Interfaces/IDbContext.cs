using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IDbContext
    {
        DbSet<Flight> Flights { get; }
        DbSet<City> Cities { get; }

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
