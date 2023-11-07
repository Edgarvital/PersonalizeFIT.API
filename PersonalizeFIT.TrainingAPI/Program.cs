using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using PersonalizeFIT.TrainingAPI.Config;
using TrainingAPI.Connectors.Database;
using PersonalizeFIT.TrainingAPI.Middlewares;
using TrainingAPI.Business.TrainingGroup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
    options.AddPolicy("Student", policy => policy.RequireClaim("realm_roles", "student-role"));
});

builder.Services.AddKeycloakAdminHttpClient(adminClientOptions);

builder.Services.AddDbContext<TrainingDbContext>(options =>
    options.UseNpgsql(builder.Configuration["AppSettings:DB_CONN"],
        optionsBuilder =>
        {
            optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", TrainingDbContext.Schema);
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

builder.Services.AddMemoryCache();

builder.Services.AddTransient<ErrorHandlerMiddleware>();

builder.Services.AddScoped(typeof(IGetAllTrainingGroups), typeof(GetAllTrainingGroups));
builder.Services.AddScoped(typeof(IGetTrainingGroup), typeof(GetTrainingGroup));
builder.Services.AddScoped(typeof(IPostTrainingGroup), typeof(PostTrainingGroup));
builder.Services.AddScoped(typeof(IUpdateTrainingGroup), typeof(UpdateTrainingGroup));
builder.Services.AddScoped(typeof(IDeleteTrainingGroup), typeof(DeleteTrainingGroup));

builder.Services.AddScoped(typeof(IGetAllTrainingPresets), typeof(GetAllTrainingPresets));
builder.Services.AddScoped(typeof(IGetTrainingPreset), typeof(GetTrainingPreset));
builder.Services.AddScoped(typeof(IPostTrainingPreset), typeof(PostTrainingPreset));
builder.Services.AddScoped(typeof(IUpdateTrainingPreset), typeof(UpdateTrainingPreset));
builder.Services.AddScoped(typeof(IDeleteTrainingPreset), typeof(DeleteTrainingPreset));

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
