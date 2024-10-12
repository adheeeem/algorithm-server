using System.Net;

namespace WebAPI.Exceptions;

public class ErrorModel
{
	public HttpStatusCode StatusCode { get; set; }
	public string? Message { get; set; }
	public string? Details { get; set; }
}
