using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class AuthenticationMiddleware
{
	private readonly RequestDelegate _next;

	public AuthenticationMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		if (!context.User.Identity.IsAuthenticated)
		{
			context.Response.Redirect("/Home/NotFound");
			return;
		}

		await _next(context);
	}
}
