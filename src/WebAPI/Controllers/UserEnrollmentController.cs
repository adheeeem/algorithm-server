using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserEnrollmentController(UserEnrollmentService userEnrollmentService) : ControllerBase
	{
		[Authorize(ApplicationPolicies.Student)]
		[HttpGet]
		[Route("enroll/{unitNumber:int}")]
		public async Task<IActionResult> UserEnrollment(int unitNumber)
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();
			var response = await userEnrollmentService.GetUserEnrollmentResponse(int.Parse(id), unitNumber);
			return Ok(response);
		}
		  
		[Authorize(ApplicationPolicies.Student)]
		[HttpPost]
		[Route("enroll/{unitNumber:int}")]
		public async Task<IActionResult> EnrollUser(int unitNumber)
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();

			await userEnrollmentService.EnrollUser(int.Parse(id), unitNumber);
			return Ok();
		}

		[Authorize(ApplicationPolicies.Administrator)]
		[HttpPost]
		[Route("paid-status")]
		public async Task<IActionResult> UpdateUserEnrollmentPaidStatus([FromQuery] int userId, [FromQuery] int unitNumber, [FromQuery] bool isPaid)
		{
			await userEnrollmentService.UpdateUserEnrollmentPaidStatus(userId, unitNumber, isPaid);
			return Ok();
		}
	}
}
