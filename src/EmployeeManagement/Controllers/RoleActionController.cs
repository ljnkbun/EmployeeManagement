using Application.Commands.RoleActions;
using Application.Parameters.RoleActions;
using Application.Queries.RoleActions;
using EmployeeManagement.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class RoleActionController : BaseApiController
    {
        // GET: api/v1/<controller>
        [HttpGet]
        [CusAuthorize]
        public async Task<IActionResult> Get([FromQuery] RoleActionParameter filter)
        {
            return Ok(await Mediator.Send(new GetRoleActionsQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                Controller = filter.Controller,
                Action = filter.Action,
                RoleId = filter.RoleId,
            }));
        }

        // GET api/v1/<controller>/5
        [HttpGet("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetRoleActionQuery { Id = id }));
        }

        // POST api/v1/<controller>
        [HttpPost]
        [CusAuthorize]
        public async Task<IActionResult> Post(CreateRoleActionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/<controller>/5
        [HttpPut("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Put(int id, UpdateRoleActionCommand command)
        {
            if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/v1/<controller>/5
        [HttpDelete("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteRoleActionCommand { Id = id }));
        }
    }
}

