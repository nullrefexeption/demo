using Application.Interfaces;
using AutoMapper;
using MediatR;
using Models.Models;
using Models.Models.Table;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Queries
{
    public class GetFlightsQuery : IRequest<TableData<FlightDto>>
    {
        public TableParams Params { get; set; }
    }

    class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, TableData<FlightDto>>
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

        public async Task<TableData<FlightDto>> Handle(GetFlightsQuery query, CancellationToken cancellationToken = default)
        {
            var tableData = new TableData<FlightDto>();
            var records = await _flightsService.GetAllFlights(query.Params);

            var dtos = _mapper.Map<List<FlightDto>>(records.Data);
            tableData.Data = dtos;
            tableData.Count = records.Count;

            return tableData;
        }
    }
}
