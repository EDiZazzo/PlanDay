using Microsoft.AspNetCore.Mvc;
using Planday.Schedule.Request;
using Planday.Schedule.Infrastructure.Services;
using Planday.Schedule.Queries;
using Planday.Schedule.Response;

namespace Planday.Schedule.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        [HttpGet("{shiftId}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetShiftById(
            [FromServices] IGetEntityByIdQuery<ShiftId, Shift> getShiftById,
            [FromServices] IGetEmployeeByExternalApiService getExternalEmployee,
            [FromRoute] string shiftId)
        {
            var id = ShiftId.New(shiftId);

            var shift = await getShiftById.QueryFirstOrDefaultAsync(id);
            
            var extEmployee = shift is not null ? await getExternalEmployee.GetEmployee(new EmployeeId(shift.EmployeeId.Value)) : null;

            return shift is null
            ? NotFound("Shift Id not found!")
            : Ok(new GetShiftResponse(shift, extEmployee.Email));
        }

        [HttpPost("newshift")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> NewOpenShift(
            [FromServices] INewOpenShiftService insertNewShiftService,
            [FromBody] InsertShiftRequest request)
        {
            var success = await insertNewShiftService.InsertNewShift(request.ToCommand());

            return success
            ? Ok("Shift got created successfully!")
            : BadRequest("Incorrect start and end dates");
        }

        [HttpPost("assignshift")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AssignShift(
            [FromServices] IAssignShiftService assignShiftService,
            [FromBody] AssignShiftRequest request)
        {
            var success = await assignShiftService.UpdateShift(request.ToCommand());

            return success
            ? Ok("Shift got assigned to an Employee successfully!")
            : BadRequest();
        }
    }    
}

