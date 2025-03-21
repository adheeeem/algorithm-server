﻿using Application.Interfaces.Repository;
using Application.Storage;
using Azure.Storage.Blobs;
using Infrastructure.Repository;
using Infrastructure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using Application.Interfaces;

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
        services.AddScoped<IUserEnrollmentRepository, UserEnrollmentRepository>();
        services.AddScoped<IUserWeeklyActivityRepository, UserWeeklyActivityRepository>();
        services.AddScoped<IQuestionAttemptRepository, QuestionAttemptRepository>();
        services.AddScoped<IAttemptResultRepository, AttemptResultRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}