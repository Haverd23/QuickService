
using QUS.Workflows.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDependencyInjection(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddControllers();




var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("CORS");



app.UseAuthorization();

app.MapControllers();

app.Run();
