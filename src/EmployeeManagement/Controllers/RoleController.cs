using Application.Commands.Roles;
using Application.Parameters.Roles;
using Application.Queries.Roles;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class RoleController : BaseApiController
    {
        // GET: api/v1/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] RoleParameter filter)
        {
            return Ok(await Mediator.Send(new GetRolesQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                Code = filter.Code,
                Name = filter.Name,
            }));
        }

        // GET api/v1/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetRoleQuery { Id = id }));
        }

        // POST api/v1/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateRoleCommand command)
        {
            if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/v1/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteRoleCommand { Id = id }));
        }
    }
}

