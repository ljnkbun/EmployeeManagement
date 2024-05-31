using Application.Extensions;
using Core.Settings;
using Domain.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Extensions
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jWTSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<JWTSettings> options)
        {
            _next = next;
            _jWTSettings = options.Value;
        }

        public async Task Invoke(HttpContext context, IAppUserRepository appUserRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, appUserRepository, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IAppUserRepository repository, string token)
        {
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jWTSettings.SecretKey!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clock skew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);


                var checkSumToken = GlobalCache.TokenStorages.GetValueOrDefault(userId);
                if (checkSumToken == null || checkSumToken != token) return;

                //Attach user to context on successful JWT validation
                var user = await repository.GetByIdAsync(userId);
                var identity = new ClaimsIdentity(
                [
                    new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer32),
                    new Claim("username", user.Username, ClaimValueTypes.String)
                ], "Custom");

                context.User = new ClaimsPrincipal(identity);
            }
            catch
            {
                //Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }
    }
}
