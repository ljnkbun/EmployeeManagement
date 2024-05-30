using Application.Extensions;
using Application.Parameters.AppUsers;
using Application.Queries.AppUsers;
using Core.Models.Response;
using EmployeeManagement.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AuthController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthParameter authParameter)
        {
            return Ok(await Mediator.Send(new AuthQuery()
            {
                Username = authParameter.Username,
                Password = authParameter.Password,
            }));
        }

        [CusAuthorize]
        [HttpPost("logout/{userId}")]
        public async Task<IActionResult> Logout(int userId)
        {
            var check = GlobalCache.TokenStorages.Remove(userId, out _);

            return Ok(new Response<bool>(check));
        }
    }
}
