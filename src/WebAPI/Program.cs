using System.Text;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(cfg =>
{
	cfg.Filters.Add(typeof(CustomExceptionHandler));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Algorithm Server API",
	});
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});

// Sentry
builder.WebHost.UseSentry(o =>
{
	o.Dsn = "https://482a51ecb624492e82605744d2ba3ee6@o4508132314710016.ingest.de.sentry.io/4508132358684752";
	o.Debug = true;
	o.TracesSampleRate = 1;
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("CORS_POLICY", policyConfig =>
	{
		policyConfig.WithOrigins("http://localhost:5173", "http://192.168.1.4:5173", "http://192.168.1.4", "https://algorithm-client.netlify.app", "http://109.75.62.246:5173", "http://109.75.62.246", "http://localhost:5173", "http://192.168.1.52:5173", "http://192.168.1.52", "http://192.168.1.52:5173", "http://172.18.80.1", "http://172.18.80.1:5173")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});
builder.Logging.AddConfiguration(builder.Configuration).AddSentry();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(opt =>
	{
		opt.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSecret"]!)),
			ValidateIssuer = false,
			ValidateAudience = false,
		};
	});

builder.Services.AddAuthorization(opt =>
{
	opt.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();

	opt.AddPolicy(ApplicationPolicies.SuperAdmin,
		op => op.RequireRole(
			ApplicationPolicies.SuperAdmin));
	opt.AddPolicy(ApplicationPolicies.Administrator,
		op => op.RequireRole(
			ApplicationPolicies.SuperAdmin,
		ApplicationPolicies.Administrator));
	opt.AddPolicy(ApplicationPolicies.Editor,
		op => op.RequireRole(
			ApplicationPolicies.Editor,
		ApplicationPolicies.Administrator,
		ApplicationPolicies.SuperAdmin));
	opt.AddPolicy(ApplicationPolicies.Student,
		op => op.RequireRole(
			ApplicationPolicies.Editor,
		ApplicationPolicies.Administrator,
		ApplicationPolicies.SuperAdmin,
		ApplicationPolicies.Student));
});

builder.WebHost.ConfigureKestrel(options =>
{
	options.ListenAnyIP(5220);
	options.ListenAnyIP(7117, listenOptions =>
	{
		listenOptions.UseHttps();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();
app.UseCors("CORS_POLICY");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
