using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Web.UseCases.City.Dto;

namespace Web.UseCases.City.Queries
{
    public class GetCitiesQuery : IRequest<List<CityDto>>
    {
    }

    public class MyClGetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, List<CityDto>>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public MyClGetCitiesQueryHandler(IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _dbContext.Cities.ToListAsync(cancellationToken: cancellationToken);
            var cityDtos = _mapper.Map<List<CityDto>>(cities);

            return cityDtos;
        }
    }
}
