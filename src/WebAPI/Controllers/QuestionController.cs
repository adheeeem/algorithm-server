using Application.Requests;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController(QuestionService questionService) : ControllerBase
	{
		[Authorize(ApplicationPolicies.Editor)]
		[HttpPost]
		public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionRequest request)
		{
			var questionId = await questionService.AddNewQuestion(request);
			return Ok(questionId);
		}

		[Authorize(ApplicationPolicies.Student)]
		[HttpGet]
		public async Task<IActionResult> GetAllQuestions([FromQuery] int limit, [FromQuery] int page, [FromQuery] int weekNumber, [FromQuery] int unitNumber)
		{
			var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
			if (id == null)
				return Unauthorized();
			var result = await questionService.GetAllQuestions(limit, page, weekNumber, unitNumber, int.Parse(id));
			return Ok(result);
		}

		[Authorize(ApplicationPolicies.Editor)]
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await questionService.DeleteQuestion(id);
			return Ok();
		}

		[Authorize(ApplicationPolicies.Editor)]
		[HttpPost]
		[Route("upload-image/{id:int}")]
		public async Task<IActionResult> UploadQuestionImage(int id, IFormFile file)
		{
			await questionService.UploadQuestionImage(file.OpenReadStream(), file.ContentType, id);
			return Ok();
		}
	}
}
