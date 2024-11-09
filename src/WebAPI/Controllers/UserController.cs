using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		[Authorize("Administrator")]
		[Route("register")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
		{
			int id = await _userService.Register(request);
			return Ok(id);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
		{
			var response = await _userService.Login(request);
			return Ok(response);
		}

		[HttpGet]
		[Authorize("Student")]
		public async Task<IActionResult> GetUser()
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();
			var user = await _userService.GetUser(int.Parse(id!));
			return Ok(user);
		}
	}
}
