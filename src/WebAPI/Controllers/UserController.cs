﻿using Application.Requests;
using Application.Services;
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
		[Route("register")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
		{
			int id = await _userService.CreateUser(request);
			return Ok(id);
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
		{
			var response = await _userService.Login(request);
			return Ok(response);
		}
	}
}
