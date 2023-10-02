using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Data;
using ProjektDaniel.services;
using Serilog;
using ProjektDaniel.Services;
using Microsoft.AspNetCore.Identity;
using ProjektDaniel.Models;
using Microsoft.AspNet.Identity;
using FluentValidation;
using ProjektDaniel.DTOs;
using ProjektDaniel.DTOs.Validators;
using FluentValidation.AspNetCore;
using ProjektDaniel;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("app.log")
    .MinimumLevel.Error() // Loguj tylko b³êdy i wy¿sze poziomy
    .CreateLogger();
builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



//zmiany
//app.UseAuthentication();

var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});
//do poprawy



builder.Services.AddSingleton(authenticationSettings);
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

//builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<U¿ytkownik>,PasswordHasher < U¿ytkownik >> ();
builder.Services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
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
