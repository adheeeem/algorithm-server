using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebAPI.Exceptions;

public class CustomExceptionHandler : IExceptionFilter
{
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
		}
		context.Result = new JsonResult(error);
	}
}
