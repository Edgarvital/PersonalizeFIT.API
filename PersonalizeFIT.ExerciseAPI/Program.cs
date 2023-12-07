using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using ExerciseAPI.Connectors.Database;
using PersonalizeFIT.ExerciseAPI.Config;
using PersonalizeFIT.ExerciseAPI.Middlewares;
using ExerciseAPI.Business.Exercise;
using ExerciseAPI.Business.MuscularGroup;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<ExerciseDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DB_CONN"),
        optionsBuilder =>
        {
            optionsBuilder.MigrationsHistoryTable("__EFMigrationsHistory", ExerciseDbContext.Schema);
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

builder.Services.AddScoped(typeof(IGetAllExercises), typeof(GetAllExercises));
builder.Services.AddScoped(typeof(IGetExercise), typeof(GetExercise));
builder.Services.AddScoped(typeof(IPostExercise), typeof(PostExercise));
builder.Services.AddScoped(typeof(IUpdateExercise), typeof(UpdateExercise));
builder.Services.AddScoped(typeof(IDeleteExercise), typeof(DeleteExercise));

builder.Services.AddScoped(typeof(IGetAllMuscularGroups), typeof(GetAllMuscularGroups));
builder.Services.AddScoped(typeof(IGetMuscularGroup), typeof(GetMuscularGroup));
builder.Services.AddScoped(typeof(IPostMuscularGroup), typeof(PostMuscularGroup));
builder.Services.AddScoped(typeof(IUpdateMuscularGroup), typeof(UpdateMuscularGroup));
builder.Services.AddScoped(typeof(IDeleteMuscularGroup), typeof(DeleteMuscularGroup));

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