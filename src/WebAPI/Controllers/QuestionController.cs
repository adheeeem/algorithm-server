using Application.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestionController : ControllerBase
	{
		private readonly QuestionService _questionService;

		public QuestionController(QuestionService questionService)
		{
			_questionService = questionService;
		}

		[Authorize("Editor")]
		[HttpPost]
		public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionRequest request)
		{
			var questionId = await _questionService.AddNewQuestion(request);
			return Ok(questionId);
		}

		[Authorize("Editor")]
		[HttpGet]
		public async Task<IActionResult> GetAllQuestions([FromQuery] int limit, [FromQuery] int page, [FromQuery] int weekNumber, [FromQuery] int unitNumber, [FromQuery] int grade)
		{
			var result = await _questionService.GetAllQuestions(limit, page, weekNumber, unitNumber, grade);
			return Ok(result);
		}

		[Authorize("Editor")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await _questionService.DeleteQuestion(id);
			return Ok();
		}

		[Authorize("Editor")]
		[HttpPost]
		[Route("upload-image/{id}")]
		public async Task<IActionResult> UploadProductImage(int id, IFormFile file)
		{
			await _questionService.UploadQuestionImage(file.OpenReadStream(), file.ContentType, id);
			return Ok();
		}
	}
}
