using Infrastructure;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
	options.AddPolicy("CORS_POLICY", policyConfig =>
	{
		policyConfig.WithOrigins("http://localhost:5173", "http://192.168.1.4:5173", "http://192.168.1.4", "https://algorithm-client.netlify.app", "http://109.75.62.246:5173", "http://109.75.62.246", "http://localhost:5173", "http://192.168.1.52:5173", "http://192.168.1.52", "http://192.168.1.52:5173")
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});

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
