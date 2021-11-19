using Microsoft.EntityFrameworkCore;
using UserApi.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

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

app.UseAuthorization();

app.MapControllers();

UserDbSeeder.Seed(app.Services);

app.Run();
