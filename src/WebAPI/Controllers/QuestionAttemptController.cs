using Application.Requests;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionAttemptController(QuestionAttemptService questionAttemptService) : Controller
{
    [Authorize(ApplicationPolicies.Student)]
    [HttpPost]
    public async Task<IActionResult> SubmitQuestionAttempt([FromBody] List<QuestionAttemptRequest> attempts)
    {
        var id = Request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        if (id == null)
            return Unauthorized();
        await questionAttemptService.SubmitQuestionAttempt(attempts, int.Parse(id));
        return Ok();
    }
}