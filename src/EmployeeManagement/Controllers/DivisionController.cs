using Application.Commands.Divisions;
using Application.Parameters.Divisions;
using Application.Queries.Divisions;
using EmployeeManagement.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DivisionController : BaseApiController
    {
        // GET: api/v1/<controller>
        [HttpGet]
        [CusAuthorize]
        public async Task<IActionResult> Get([FromQuery] DivisionParameter filter)
        {
            return Ok(await Mediator.Send(new GetDivisionsQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                Name = filter.Name,
            }));
        }

        // GET api/v1/<controller>/5
        [HttpGet("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDivisionQuery { Id = id }));
        }

        // POST api/v1/<controller>
        [HttpPost]
        [CusAuthorize]
        public async Task<IActionResult> Post(CreateDivisionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/v1/<controller>/5
        [HttpPut("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Put(int id, UpdateDivisionCommand command)
        {
            if (id != command.Id) return BadRequest();
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/v1/<controller>/5
        [HttpDelete("{id}")]
        [CusAuthorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteDivisionCommand { Id = id }));
        }
    }
}

