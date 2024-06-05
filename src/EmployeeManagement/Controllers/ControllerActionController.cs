using Application.Commands.ControllerActions;
using Application.Parameters.ControllerActions;
using Application.Queries.ControllerActions;
using EmployeeManagement.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class ControllerActionController : BaseApiController
    {
        // GET: api/v1/<controller>
        [HttpGet]
        [CusAuthorize]
        public async Task<IActionResult> Get([FromQuery] ControllerActionParameter filter)
        {
            return Ok(await Mediator.Send(new GetControllerActionsQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                Controller = filter.Controller,
                Action = filter.Action,
                Name = filter.Name,
                Code = filter.Code,
            }));
        }

        // GET api/v1/<controller>/5
        [HttpGet("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetControllerActionQuery { Id = id }));
        }

        // POST api/v1/<controller>
        [HttpPost]
        [CusAuthorize]
        public async Task<IActionResult> Post(CreateControllerActionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/<controller>/5
        [HttpPut("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Put(int id, UpdateControllerActionCommand command)
        {
            if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/v1/<controller>/5
        [HttpDelete("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteControllerActionCommand { Id = id }));
        }
    }
}

