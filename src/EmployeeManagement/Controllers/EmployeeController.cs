using Application.Parameters;
using Application.Queries.Employees;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : BaseApiController
    {
        // GET: api/v1/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EmployeeParameter filter)
        {
            return Ok(await Mediator.Send(new GetEmployeesQuery()
            {
                PageSize = filter.PageSize,
                PageNumber = filter.PageNumber,
                Username = filter.Username,
                Name = filter.Name,
            }));
        }

        //// GET api/v1/<controller>/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    return Ok(await Mediator.Send(new GetEmployeeQuery { Id = id }));
        //}

        //// POST api/v1/<controller>
        //[HttpPost]
        //public async Task<IActionResult> Post(CreateEmployeeCommand command)
        //{
        //    return Ok(await Mediator.Send(command));
        //}

        //// PUT api/v1/<controller>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, UpdateEmployeeCommand command)
        //{
        //    if (id != command.Id) return BadRequest();
        //    return Ok(await Mediator.Send(command));
        //}

        //// DELETE api/v1/<controller>/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    return Ok(await Mediator.Send(new DeleteEmployeeCommand { Id = id }));
        //}
    }
}

