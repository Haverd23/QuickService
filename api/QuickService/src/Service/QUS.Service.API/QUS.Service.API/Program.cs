using Microsoft.EntityFrameworkCore;
using QUS.Service.API.Extensions;
using QUS.Services.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.WithOrigins("http://localhost:4200",
              "http://localhost")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);





var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
    context.Database.Migrate();
}
app.UseCors("CORS");


// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
