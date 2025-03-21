﻿using Application.Responses;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServices
{
	public static void AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<SchoolService>();
		services.AddScoped<QuestionService>();
		services.AddScoped<WeekService>();
		services.AddScoped<UserService>();
		services.AddScoped<UserEnrollmentService>();
		services.AddScoped<WeeklyActivityService>();
		services.AddScoped<QuestionAttemptService>();
		services.AddScoped<AttemptResultService>();
	}
}
