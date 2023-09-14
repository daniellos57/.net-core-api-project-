using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbEntityValidationException ex)
        {
            // Obsługa błędów walidacji encji
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Błąd walidacji danych w bazie");
            _logger.LogError(ex, "Błąd w DbEntityValidationException");
        }
        catch (DbUpdateException ex)
        {
            context.Response.StatusCode = 404; // Ustaw kod statusu HTTP 404 (Not Found)
            await context.Response.WriteAsync("Brak ID w bazie");
            _logger.LogError(ex, "Błąd DbUpdateException");
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"Wystąpił błąd: {ex.Message}");
            _logger.LogError(ex, "Ogólny błąd");
        }
    }
    private bool IsEntityFrameworkError(Exception ex)
    {
        while (ex != null)
        {
            if (ex.GetType().Namespace.StartsWith("Microsoft.EntityFrameworkCore"))
            {
                return true;
            }
            ex = ex.InnerException;
        }
        return false;
    }

}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}

