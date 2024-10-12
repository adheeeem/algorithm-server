using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WeekController : ControllerBase
	{
		private readonly WeekService _weekService;

		public WeekController(WeekService weekService)
		{
			_weekService = weekService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateWeek([FromBody] CreateWeekRequest request)
		{
			var weekId = await _weekService.CreateWeek(request);
			return Ok(weekId);
		}
	}
}
