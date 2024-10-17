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
		switch (context.Exception)
		{
			case RecordNotFoundException:
				error.StatusCode = HttpStatusCode.NotFound;
				break;
			case RecordAlreadyExistsException:
				error.StatusCode = HttpStatusCode.BadRequest;
				break;
			case BadRequestException:
				error.StatusCode = HttpStatusCode.BadRequest;
				break;
		}
		_logger.LogError(context.Exception, "An exception occurred: {Message}, Details: {Details}", error.Message, error.Details);

		context.Result = new JsonResult(error);
	}
}
