using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;

namespace Application.Commands.Divisions
{
    public class CreateDivisionCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
    }

    public class CreateDivisionCommandHandler : IRequestHandler<CreateDivisionCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IDivisionRepository _repository;

        public CreateDivisionCommandHandler(IMapper mapper,
            IDivisionRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDivisionCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Division>(request);
            await _repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
