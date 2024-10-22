using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
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

		[Authorize("Administrator")]
		[HttpPost]
		public async Task<IActionResult> AddNewSchool([FromBody] CreateSchoolRequest request)
		{
			var schoolId = await _schoolService.AddNewSchool(request);
			return Ok(schoolId);
		}

		[HttpGet]
		[Authorize("Administrator")]
		public async Task<IActionResult> GetSchools([FromQuery] int limit = 0, [FromQuery] int page = 0, [FromQuery] string name = "", [FromQuery] string region = "", [FromQuery] string city = "", [FromQuery] string country = "")
		{
			var response = await _schoolService.GetSchools(limit, page, name, region, city, country);
			return Ok(response);
		}

	}
}
