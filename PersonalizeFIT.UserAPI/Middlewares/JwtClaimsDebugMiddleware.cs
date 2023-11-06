public class JwtClaimsDebugMiddleware
{
    private readonly RequestDelegate _next;

    public JwtClaimsDebugMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Obtenha as reivindicações do token atual
        var user = context.User;
        if (user.Identity.IsAuthenticated)
        {
            Console.WriteLine("Claims from JWT token:");
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
        }

        // Continue com a solicitação
        await _next(context);
    }
}
