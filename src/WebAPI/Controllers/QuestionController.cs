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
		public async Task<IActionResult> AddNewSchool([FromBody] AddNewQuestionRequest request)
		{
			var questionId = await _questionService.AddNewQuestion(request);
			return Ok(questionId);
		}
	}
}
