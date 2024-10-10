using Application.Interfaces;
using Infrastructure.Repository;
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

		services.AddScoped<ISchoolRepository, SchoolRepository>();
	}
}
