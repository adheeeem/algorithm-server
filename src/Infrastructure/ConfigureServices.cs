using Application.Interfaces;
using Application.Storage;
using Azure.Storage.Blobs;
using Infrastructure.Repository;
using Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace Infrastructure;

public static class ConfigureServices
{
	public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddTransient<IDbConnection>(dc => new NpgsqlConnection(configuration["DefaultConnection"]));

		services.AddSingleton(x => new BlobServiceClient(configuration["AzureBlobStorage"]));
		services.AddScoped<IBlobService, BlobService>();

		services.AddScoped<ISchoolRepository, SchoolRepository>();
		services.AddScoped<IQuestionRepository, QuestionRepository>();
		services.AddScoped<IWeekRepository, WeekRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
	}
}
