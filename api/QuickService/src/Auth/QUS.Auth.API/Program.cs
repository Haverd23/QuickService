using QUS.Auth.API.Extensions;
using QUS.Core.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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


builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();
app.UseCors("CORS");


app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
