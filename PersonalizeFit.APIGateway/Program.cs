using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var authenticationOptions = builder
                            .Configuration
                            .GetSection(KeycloakAuthenticationOptions.Section)
                            .Get<KeycloakAuthenticationOptions>();

builder.Services.AddKeycloakAuthentication(authenticationOptions);

var authorizationOptions = builder
                            .Configuration
                            .GetSection(KeycloakProtectionClientOptions.Section)
                            .Get<KeycloakProtectionClientOptions>();

builder.Services.AddKeycloakAuthorization(authorizationOptions);

var adminClientOptions = builder
                            .Configuration
                            .GetSection(KeycloakAdminClientOptions.Section)
                            .Get<KeycloakAdminClientOptions>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Trainer", policy => policy.RequireClaim("realm_roles", "trainer-role"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("realm_roles", "admin-role"));
    options.AddPolicy("Student", policy => policy.RequireClaim("realm_roles", "student-role"));
});

builder.Services.AddKeycloakAdminHttpClient(adminClientOptions);

builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot();

app.Run();