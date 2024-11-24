using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserWeeklyActivityController(WeeklyActivityService weeklyActivityService) : Controller
{
    [Authorize(ApplicationPolicies.Student)]
    [HttpGet("{unitNumber:int}")]
    public async Task<IActionResult> GetUnitWeeksAccess(int unitNumber)
    {
        var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (id == null)
            return Unauthorized();
        var result = await weeklyActivityService.GetUnitWeeksAccess(int.Parse(id), unitNumber);
        return Ok(result);
    }
}