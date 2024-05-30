using Application.Extensions;
using Application.Models.AppUsers;
using AutoMapper;
using Core.Exceptions;
using Core.Models.Response;
using Core.Settings;
using Domain.Interface;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Queries.AppUsers
{
    public class AuthQuery : IRequest<Response<AppUserModel>>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class AuthQueryHandler : IRequestHandler<AuthQuery, Response<AppUserModel>>
    {
        private readonly IAppUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jWTSettings;

        public AuthQueryHandler(IAppUserRepository repository
            , IOptions<JWTSettings> options
            , IMapper mapper
            )
        {
            _repository = repository;
            _mapper = mapper;
            _jWTSettings = options.Value;
        }

        public async Task<Response<AppUserModel>> Handle(AuthQuery query, CancellationToken cancellationToken)
        {
            var user = await _repository.ValidateUser(query.Username, query.Password) ?? throw new ApiException($"Username or password is incorrect!");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jWTSettings.SecretKey!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("id", user.Id.ToString())
                ]),
                Expires = DateTime.UtcNow.AddMinutes((double)_jWTSettings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //cache token for logout
            GlobalCache.TokenStorages.AddOrUpdate(user.Id, tokenString, (key, oldValue) => tokenString);

            return new Response<AppUserModel>(new AppUserModel() { Id = user.Id, Token = tokenString });

        }
    }
}
