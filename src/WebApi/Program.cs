using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using UserApi.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

//builder.Services.AddHealthChecks();

//builder.Services.AddHealthChecks().AddDbContextCheck<UserDbContext>();
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.MapGet("/", () =>
{
    return "kt-test";
});

//app.MapGet("/health", () =>
//{
//    return new { status = "OK" };
//});

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

//app.UseEndpoints(endpoints =>
//{
//    //A failed liveness probe says: The application has crashed. You should shut it down and restart.
//    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
//    {
//        ResultStatusCodes =
//        {
//            [HealthStatus.Healthy] = StatusCodes.Status200OK,
//            [HealthStatus.Degraded] = StatusCodes.Status200OK,
//            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
//        }
//    });
//    //A failed readiness probe says: The application is OK but not yet ready to serve traffic.
//    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
//    {
//        ResultStatusCodes =
//        {
//            [HealthStatus.Healthy] = StatusCodes.Status200OK,
//            [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
//            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
//        }
//    });
//});

UserDbSeeder.Seed(app.Services);

app.Run();
