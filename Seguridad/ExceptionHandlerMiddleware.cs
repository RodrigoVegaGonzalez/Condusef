using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var originalPath = context.Request.Path; // Guarda la ruta original antes de cualquier redirección

        await _next(context);

        // Evitar redirecciones si ya se ha redirigido
        if (!context.Response.HasStarted && context.Request.Path != originalPath)
            return;

        if (context.Response.StatusCode == 404 && !IsAlreadyRedirected(context))
        {
            context.Response.Redirect("/error/not-found");
        }
        else if ((context.Response.StatusCode == 401 || context.Response.StatusCode == 403) && !IsAlreadyRedirected(context))
        {
            context.Response.Redirect("/error/access-denied");
        }
        else if (context.Response.StatusCode >= 500 && !IsAlreadyRedirected(context))
        {
            context.Response.Redirect("/error/server-error");
        }
    }

    // Método auxiliar para verificar si ya se ha redirigido
    private bool IsAlreadyRedirected(HttpContext context)
    {
        return context.Response.StatusCode == 302 && context.Response.Headers.ContainsKey("Location");
    }
}
