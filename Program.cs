using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Data;
using ProjektDaniel.services;
using Serilog;
using ProjektDaniel.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("app.log")
    .MinimumLevel.Error() // Loguj tylko b³êdy i wy¿sze poziomy
    .CreateLogger();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<U¿ytkownikDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("projektlokalnie")));

builder.Services.AddDbContext<RolaDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("projektlokalnie")));

builder.Services.AddDbContext<WniosekDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("projektlokalnie")));

builder.Services.AddDbContext<DaneWniosekDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("projektlokalnie")));

builder.Services.AddScoped<U¿ytkownikService>();
builder.Services.AddScoped<WniosekService>();
builder.Services.AddScoped<DaneService>();

builder.Services.AddLogging(logging =>
{
    logging.AddSerilog();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseErrorHandlingMiddleware();

app.MapControllers();

app.Run();
