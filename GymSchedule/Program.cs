using GymSchedule.Models;
using GymSchedule.RequestBody;
using GymSchedule.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<SportActivityDatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<SportActivityService>();
// Add services to the container.
builder.Services.AddScoped<IActivityBody, ActivityBody>();
//builder.Services.AddScoped<ISportActivityDatabaseSettings, SportActivityDatabaseSettings>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
