using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebAPI.Exceptions;

public class CustomExceptionHandler : IExceptionFilter
{
	private readonly ILogger<CustomExceptionHandler> _logger;

	public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
	{
		_logger = logger;
	}
	public void OnException(ExceptionContext context)
	{
		var error = new ErrorModel
		{
			StatusCode = HttpStatusCode.InternalServerError,
			Message = context.Exception.Message,
			Details = context.Exception.ToString()
		};
		error.StatusCode = context.Exception switch
		{
			RecordNotFoundException => HttpStatusCode.NotFound,
			RecordAlreadyExistsException => HttpStatusCode.BadRequest,
			BadRequestException => HttpStatusCode.BadRequest,
			_ => error.StatusCode
		};
		_logger.LogError(context.Exception, "An exception occurred: {Message}, Details: {Details}", error.Message, error.Details);

		context.Result = new JsonResult(error);
	}
}
