using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Business.Authorization
{
    public static class AuthorizationBearerDefaults
    {
        public const string User = "UserBearer";

        public static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder authBuilder, WebApplicationBuilder builder, string? environment)
        {
            authBuilder.AddJwtBearer(User, options => {
                options.Authority = builder.Configuration["KeyCloak:AuthorityUser"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                };
                options.BackchannelHttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = delegate
                    {
                        return environment != null;
                    }
                };
                options.RequireHttpsMetadata = false;
            });

            return authBuilder;
        }

    }
}
