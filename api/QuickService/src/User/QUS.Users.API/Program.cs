using Microsoft.EntityFrameworkCore;
using QUS.Core.Exceptions;
using QUS.Users.API.Extensions;
using QUS.Users.Application;
using QUS.Users.Data;
using QUS.Users.Data.Repository;
using QUS.Users.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDependencyInjection(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    context.Database.Migrate();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
