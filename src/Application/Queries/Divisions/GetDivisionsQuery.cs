using Application.Models.Divisions;
using Application.Parameters.Divisions;
using AutoMapper;
using Core.Models.Response;
using Domain.Interface;
using MediatR;

namespace Application.Queries.Divisions
{
    public class GetDivisionsQuery : IRequest<PagedResponse<IReadOnlyList<DivisionModel>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? OrderBy { get; set; }
    }

    public class GetDivisionsQueryHandler : IRequestHandler<GetDivisionsQuery, PagedResponse<IReadOnlyList<DivisionModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IDivisionRepository _repository;

        public GetDivisionsQueryHandler(IMapper mapper,
            IDivisionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PagedResponse<IReadOnlyList<DivisionModel>>> Handle(GetDivisionsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<DivisionParameter>(request);
            return await _repository.GetModelPagedReponseAsync<DivisionParameter, DivisionModel>(validFilter);
        }
    }
}
