using Infrastructure;
using Application;
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

builder.WebHost.UseSentry(o =>
{
	o.Dsn = "https://482a51ecb624492e82605744d2ba3ee6@o4508132314710016.ingest.de.sentry.io/4508132358684752";
	o.Debug = true;
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

SentrySdk.CaptureMessage("Hello Sentry");

builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

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
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CORS_POLICY");
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
