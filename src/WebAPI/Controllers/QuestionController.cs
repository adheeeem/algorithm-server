using Application.Requests;
using Application.Services;
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

		[HttpPost]
		public async Task<IActionResult> AddNewSchool([FromBody] CreateQuestionRequest request)
		{
			var questionId = await _questionService.AddNewQuestion(request);
			return Ok(questionId);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllQuestions([FromQuery] int limit, [FromQuery] int page, [FromQuery] int weekNumber, [FromQuery] int unitNumber, [FromQuery] int grade)
		{
			var result = await _questionService.GetAllQuestions(limit, page, weekNumber, unitNumber, grade);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteQuestion(int id)
		{
			await _questionService.DeleteQuestion(id);
			return Ok();
		}
	}
}
