using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SchoolController : ControllerBase
	{
		private readonly SchoolService _schoolService;

		public SchoolController(SchoolService schoolService)
		{
			_schoolService = schoolService;
		}

		[HttpPost]
		public async Task<IActionResult> AddNewSchool([FromBody] CreateSchoolRequest request)
		{
			var schoolId = await _schoolService.AddNewSchool(request);
			return Ok(schoolId);
		}

	}
}
