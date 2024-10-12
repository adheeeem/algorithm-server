using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServices
{
	public static void AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<SchoolService>();
		services.AddScoped<QuestionService>();
	}
}
