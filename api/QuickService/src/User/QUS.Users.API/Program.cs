using Microsoft.EntityFrameworkCore;
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



app.UseAuthorization();

app.MapControllers();

app.Run();
