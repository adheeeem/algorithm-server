using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserEnrollmentController : ControllerBase
	{
		private readonly UserEnrollmentService _userEnrollmentService;

		public UserEnrollmentController(UserEnrollmentService userEnrollmentService)
		{
			_userEnrollmentService = userEnrollmentService;
		}

		[Authorize("Student")]
		[HttpGet]
		[Route("enroll/{unitNumber}")]
		public async Task<IActionResult> UserEnrollment(int unitNumber)
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();
			var response = await _userEnrollmentService.GetUserEnrollmentResponse(int.Parse(id), unitNumber);
			return Ok(response);
		}

		[Authorize("Student")]
		[HttpPost]
		[Route("enroll/{unitNumber}")]
		public async Task<IActionResult> EnrollUser(int unitNumber)
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();

			await _userEnrollmentService.EnrollUser(int.Parse(id), unitNumber);
			return Ok();
		}

		[Authorize("Administrator")]
		[HttpPost]
		[Route("paid-status")]
		public async Task<IActionResult> UpdateUserEnrollmentPaidStatus([FromQuery] int userId, [FromQuery] int unitNumber, [FromQuery] bool isPaid)
		{
			await _userEnrollmentService.UpdateUserEnrollmentPaidStatus(userId, unitNumber, isPaid);
			return Ok();
		}
	}
}
