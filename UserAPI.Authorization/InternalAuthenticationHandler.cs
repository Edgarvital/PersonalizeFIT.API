using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Business.Authorization
{
    public class InternalAuthenticationSchema
    {
        public const string SchemeName = "Internal";
    }
    public class InternalAuthenticationOptions : AuthenticationSchemeOptions
    {

    }

    public class InternalAuthenticationHandler : AuthenticationHandler<InternalAuthenticationOptions>
    {
        private readonly ILogger<InternalAuthenticationHandler> _logger;
        private readonly IConfiguration _config;

        public InternalAuthenticationHandler(IOptionsMonitor<InternalAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration config) : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<InternalAuthenticationHandler>();
            _config = config;
        }

        /*
         * header: 
         * header: Internal-Token => WppdM1IF7SB52jCsLGni2ponSqepwBGB 
         * header: Role => Admin
         * header: UserId => GUID
         * header: Distributor => GUID
         */
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
         

            if (!Request.Headers.ContainsKey("Role"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
                // return Task.FromResult(AuthenticateResult.Fail("Missing Role"));
            }    

            var roles = Request.Headers["Role"];
            var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

    }
}
