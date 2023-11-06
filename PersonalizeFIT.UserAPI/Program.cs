using AutoMapper;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.EntityFrameworkCore;
using PersonalizeFIT.UserAPI.Config;
using PersonalizeFIT.UserAPI.Middlewares;
using UserAPI.Business.TrainerHasStudent;
using UserAPI.Connectors.Database;
using UserAPI.Connectors.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
});

builder.Services.AddKeycloakAdminHttpClient(adminClientOptions);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration["AppSettings:DB_CONN"],
        optionsBuilder =>
        {
            optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", UserDbContext.Schema);
        }));

var allow = "allowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allow, policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddHttpClient<IAuthAPI, AuthAPI>(options =>
{
    options.BaseAddress = new Uri(builder.Configuration["KeyCloak:auth-server-url"]);
});

builder.Services.AddMemoryCache();


builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.AddScoped(typeof(IGetTrainerHasStudents), typeof(GetTrainerHasStudents));
builder.Services.AddScoped(typeof(IPostTrainerHasStudents), typeof(PostTrainerHasStudents));
builder.Services.AddScoped(typeof(IAuthAPI), typeof(AuthAPI));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(allow);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<JwtClaimsDebugMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();