using Application.Requests;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeekController(WeekService weekService) : ControllerBase
	{
		[Authorize(ApplicationPolicies.Administrator)]
		[HttpPost]
		public async Task<IActionResult> CreateWeek([FromBody] CreateWeekRequest request)
		{
			var weekId = await weekService.CreateWeek(request);
			return Ok(weekId);
		}
	}
}
