using AutoMapper;
using Core.Models.Response;
using Domain.Entities;
using Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Employees
{
    public class CreateEmployeeCommand : IRequest<Response<int>>
    {
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int Level { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _repository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateEmployeeCommandHandler(IMapper mapper
           , IEmployeeRepository repository
            , IAppUserRepository appUserRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _appUserRepository = appUserRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Employee>(request);

            var curUserId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(x => x.Type == "id")!.Value;
            var curUser = await _appUserRepository.GetByIdAsync(int.Parse(curUserId));

            if (entity.Level <= curUser.Level)
                entity.Level = curUser.Level + 1; //user only create employee level lower 

            await _repository.AddAsync(entity);
            return new Response<int>(entity.Id);
        }
    }
}
