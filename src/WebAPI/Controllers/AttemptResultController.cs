using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttemptResultController(AttemptResultService attemptResultService) : Controller
{
    [Authorize(ApplicationPolicies.Student)]
    [HttpGet]
    public async Task<IActionResult> MinimalAttemptResult([FromQuery] int weekNumber, [FromQuery] int unitNumber)
    {
        var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (id == null)
            return Unauthorized();
        var result = await attemptResultService.GetUserWeekUnitAttemptResults(weekNumber, unitNumber, int.Parse(id));
        return Ok(result);
    }
}