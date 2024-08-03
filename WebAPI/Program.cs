using WebAPI.Options;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<KeycloakConfiguration>(builder.Configuration.GetSection("KeycloakConfiguration"));

builder.Services.AddScoped<KeycloakService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapGet("/get-access-token", async (KeycloakService keycloakService) =>
{
    string token = await keycloakService.GetAccessToken(default);

    return Results.Ok(new { AccessToken = token });
});


app.MapControllers();

app.Run();
