using Application.Requests;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(UserService userService) : ControllerBase
	{
		[HttpPost]
		[Authorize(ApplicationPolicies.Administrator)]
		[Route("register")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
		{
			var id = await userService.Register(request);
			return Ok(id);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
		{
			var response = await userService.Login(request);
			return Ok(response);
		}

		[HttpGet]
		[Authorize(ApplicationPolicies.Student)]
		public async Task<IActionResult> GetUser()
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();
			var user = await userService.GetUser(int.Parse(id!));
			return Ok(user);
		}
	}
}
