using Application.Parameters.AppUsers;
using Application.Queries.AppUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AuthController : BaseApiController
    {

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(AuthParameter authParameter)
        {
            return Ok(await Mediator.Send(new AuthQuery()
            {
                Username = authParameter.Username,
                Password = authParameter.Password,
            }));
        }
    }
}
