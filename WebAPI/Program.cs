var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapGet("/", () => "Hello World!");

app.Run();
