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

		[Authorize(ApplicationPolicies.Editor)]
		[HttpGet]
		public async Task<IActionResult> GetAllQuestions([FromQuery] int limit, [FromQuery] int page, [FromQuery] int weekNumber, [FromQuery] int unitNumber, [FromQuery] int grade)
		{
			var result = await questionService.GetAllQuestions(limit, page, weekNumber, unitNumber, grade);
			return Ok(result);
		}

		[Authorize(ApplicationPolicies.Editor)]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await questionService.DeleteQuestion(id);
			return Ok();
		}

		[Authorize(ApplicationPolicies.Editor)]
		[HttpPost]
		[Route("upload-image/{id}")]
		public async Task<IActionResult> UploadProductImage(int id, IFormFile file)
		{
			await questionService.UploadQuestionImage(file.OpenReadStream(), file.ContentType, id);
			return Ok();
		}
	}
}
