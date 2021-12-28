using Application.Interfaces;
using AutoMapper;
using MediatR;
using Models.Models;
using Models.Models.Table;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Queries
{
    public class GetFlightsQuery : IRequest<Response<TableData<FlightDto>>>
    {
        public TableParams Params { get; set; }
    }

    class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, Response<TableData<FlightDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IFlightsService _flightsService;

        public GetFlightsQueryHandler(
            IMapper mapper,
            IFlightsService flightsService
            )
        {
            _mapper = mapper;
            _flightsService = flightsService;
        }

        public async Task<Response<TableData<FlightDto>>> Handle(GetFlightsQuery query, CancellationToken cancellationToken = default)
        {
            Response<TableData<FlightDto>> result = new Response<TableData<FlightDto>>();
            
            var records = await _flightsService.GetAllFlights(query.Params);

            result.Data.Data = _mapper.Map<List<FlightDto>>(records.Data);
            result.Data.Count = records.Data.Count;

            return result;
        }
    }
}
